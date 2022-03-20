using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Features.ProductTransfers.Commands.ApproveProductTransfer;
using Sintra.Application.Features.ProductTransfers.Commands.CreateProductTransfer;
using Sintra.Application.Features.ProductTransfers.Queries.GetTransferProducts;
using Sintra.Application.Interfaces;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using Sintra.Infrastructure.Persistence.Contexts;
using Sintra.Infrastructure.Persistence.Dapper;
using Sintra.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Infrastructure.Persistence.Repositories
{
    public class ProductTransferRepository : GenericRepositoryAsync<ProductTransfer>, IProductTransferRepository
    {
        private readonly DbSet<ProductTransfer> productTransfers;
        private readonly IdentityContext context;
        private readonly IMapper mapper;
        private readonly IDateTimeService dateTimeService;
        private readonly IDapper dapper;

        public ProductTransferRepository(IdentityContext dbContext, IMapper mapper, IDateTimeService dateTimeService,
            IDapper dapper) : base(dbContext, mapper)
        {
            productTransfers = dbContext.Set<ProductTransfer>();
            context = dbContext;
            this.mapper = mapper;
            this.dateTimeService = dateTimeService;
            this.dapper = dapper;
        }
        public async Task<string> RejectTransferProduct(int id, int statusId, string message)
        {
            try
            {
                var productTransfer = await GetByIdAsync(id);
                productTransfer.TransferStatusId = statusId;
                if (message != null)
                {
                    productTransfer.Message = message;
                    productTransfer.ApproveDate = dateTimeService.NowUtc;
                }
                await UpdateAsync(productTransfer);
                DynamicParameters p = new DynamicParameters();
                p.Add("value", id.ToString());
                dapper.Execute
                        (@$"update Notifications set StatusId=2
                    where Value=@value", p, CommandType.Text);

                CreateNotification(productTransfer.Id, productTransfer.FromWarehouseId, "Transfer sorgunuz rədd edildi");

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public string ApproveTransferProduct(int id, int statusId)
        {
            try
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("id", id);
                p.Add("value", id.ToString());
                p.Add("statusId", statusId);
                p.Add("approveDate", dateTimeService.NowUtc);
                dapper.Update<ProductTransfer>
                    (@$"update ProductTransfers set ApproveDate=@approveDate
                    where Id=@id", p, CommandType.Text);

                List<ProductTransferViewModel> productTransfers = dapper.GetAll<ProductTransferViewModel>
                    (@$"select pt.*,tp.ProductId,tp.count, wpto.Balance as ToPreviousBalance,
                        wpf.Balance as FromPreviousBalance
                from TransferProducts tp
                left join ProductTransfers pt on pt.Id=tp.TransferId
                left join warehouseproducts wpf on wpf.WarehouseId=pt.FromWarehouseId and wpf.ProductId=tp.ProductId
                left join WarehouseProducts wpto on wpto.WarehouseId=pt.ToWarehouseId and wpto.ProductId=tp.ProductId
                where tp.TransferId=@id", p, CommandType.Text);

                foreach (var productTransfer in productTransfers)
                {
                    DynamicParameters b = new DynamicParameters();
                    b.Add("transferId", productTransfer.Id);
                    b.Add("toWarehouseId", productTransfer.ToWarehouseId);
                    b.Add("fromWarehouseId", productTransfer.FromWarehouseId);
                    b.Add("productId", productTransfer.ProductId);
                    b.Add("toPreviousBalance", productTransfer.ToPreviousBalance);
                    b.Add("fromPreviousBalance", productTransfer.FromPreviousBalance);
                    b.Add("toPresentBalance", productTransfer.ToPreviousBalance + productTransfer.count);
                    b.Add("fromPresentBalance", productTransfer.FromPreviousBalance - productTransfer.count);

                    dapper.Execute
                        (@$"update TransferProducts set
                    FromPreviousBalance=@fromPreviousBalance,FromPresentBalance=@fromPresentBalance,
                    ToPreviousBalance=@toPreviousBalance,ToPresentBalance=@toPresentBalance
                    where TransferId=@transferId and ProductId=@productId", b, CommandType.Text);

                    dapper.Execute
                        (@$"update WarehouseProducts set Balance=@fromPresentBalance
                    where WarehouseId=@fromWarehouseId and ProductId=@productId", b, CommandType.Text);
                    WarehouseProduct toWarehouseProduct = dapper.Get<WarehouseProduct>
                        (@$"select * from WarehouseProducts
                    where WarehouseId=@toWarehouseId and ProductId=@productId", b, CommandType.Text);
                    if (toWarehouseProduct == null)
                        dapper.Execute
                        (@$"insert into warehouseproducts (WarehouseId,ProductId,Balance)
                        values(@toWarehouseId,@productId,@toPresentBalance)", b, CommandType.Text);
                    else
                        dapper.Execute
                            (@$"update WarehouseProducts set Balance=@toPresentBalance
                        where WarehouseId=@toWarehouseId and ProductId=@productId", b, CommandType.Text);
                }
                dapper.Execute
                        (@$"update Notifications set StatusId=2
                    where Value=@value", p, CommandType.Text);

                CreateNotification(productTransfers[0].Id, productTransfers[0].FromWarehouseId, "Transfer sorgunuz təsdiq edildi");

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<ProductTransfer> GetProductTransferById(int id, string userId)
        {
            ProductTransfer productTransfer = await GetByIdAsync(id);
            var warehouseUsers = context.WarehouseUsers
                                .Where(x => x.WarehouseId == productTransfer.ToWarehouseId)
                                .ToList();

            foreach (var item in warehouseUsers)
            {
                if (item.UserId == userId)
                {
                    productTransfer.TransferStatusId = 1;
                    break;
                }
                else
                {
                    productTransfer.TransferStatusId = 2;
                }
            }

            if (warehouseUsers.Count == 0)
                productTransfer.TransferStatusId = 2;

            /*if (warehouseUsers.Any(x => x.UserId != userId))
                productTransfer.TransferStatusId = 2;*/
            return productTransfer;
        }

        public List<GetTransferProductsViewModel> GetTransferProducts(int transferId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("transferId", transferId);
            List<GetTransferProductsViewModel> products = dapper.GetAll<GetTransferProductsViewModel>
                (@$"select p.Id, p.Name,p.Barcode,tp.count,p.ProductTypeId
                    from Products p
                    left join TransferProducts tp on tp.ProductId=p.Id
                    left join ProductTransfers pt on pt.Id=tp.TransferId
                    where pt.Id=@transferId", p, CommandType.Text);

            return products;
        }

        public void MakeNotificationViewed(int transferId, string userId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("value", transferId.ToString());
            p.Add("userId", userId);

            dapper.Execute
                (@$"Update nu
                    SET nu.StatusId=2
                    FROM  NotificationUsers nu
                    LEFT JOIN Notifications n ON n.Id=nu.NotificationId
                    where n.Value=@value and UserId=@userId", p, CommandType.Text);
        }

        public void CreateProductTransfer(CreateProductTransferCommand command)
        {

            Warehouse fromWarehouse = context.Warehouses.Where(x => x.Id == command.fromWarehouseId).FirstOrDefault();
            Warehouse toWarehouse = context.Warehouses.Where(x => x.Id == command.toWarehouseId).FirstOrDefault();
            if (fromWarehouse == null || toWarehouse == null)
            {
                throw new Exception("Anbar tapılmadı");
            }
            List<ProductTransfer> productTransfers = new List<ProductTransfer>();
            ProductTransfer pt = new ProductTransfer
            {
                FromWarehouseId = command.fromWarehouseId,
                ToWarehouseId = command.toWarehouseId,
                CreateDate = dateTimeService.NowUtc,
                TransferStatusId = 1
            };
            context.ProductTransfers.Add(pt);
            context.SaveChanges();

            CreateNotification(pt.Id, pt.ToWarehouseId, "Yeni transfer sorgunuz var");

            foreach (var item in command.products)
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("transferId", pt.Id);
                p.Add("productId", item.productId);
                p.Add("count", item.quantity);
                dapper.Insert<TransferProducts>
                (@$"insert into TransferProducts
                    (TransferId,ProductId,Count,FromPreviousBalance,FromPresentBalance,ToPreviousBalance,ToPresentBalance)
                    values(@transferId,@productId,@count,0,0,0,0)", p, CommandType.Text);
            }
        }

        public void CreateNotification(int id, int warehouseId, string text)
        {
            Notification notification = new Notification
            {
                Text = text,
                Value = id.ToString(),
                TypeId = 1,
                StatusId = 1
            };
            context.Notifications.Add(notification);
            context.SaveChanges();
            List<WarehouseUser> warehouseUsers = context.WarehouseUsers
                                                            .Where(x => x.WarehouseId == warehouseId)
                                                            .ToList();
            List<NotificationUser> notificationUsers = new List<NotificationUser>();
            foreach (var warehouseUser in warehouseUsers)
            {
                NotificationUser notificationUser = new NotificationUser
                {
                    UserId = warehouseUser.UserId,
                    NotificationId = notification.Id,
                    StatusId = 1
                };
                notificationUsers.Add(notificationUser);
            }
            context.NotificationUsers.AddRange(notificationUsers);
            context.SaveChanges();
        }
    }
}
