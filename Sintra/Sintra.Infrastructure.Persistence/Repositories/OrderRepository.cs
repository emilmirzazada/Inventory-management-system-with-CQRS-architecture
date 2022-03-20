using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Features.OrderAccessories.Queries.GetAllOrderAccessoriesByDate;
using Sintra.Application.Features.Orders.Commands.CreateOrder;
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
    public class OrderRepository : GenericRepositoryAsync<Order>, IOrderRepository
    {
        private readonly DbSet<Order> _orders;
        private readonly IdentityContext context;
        private readonly IDapper dapper;
        private readonly IDateTimeService dateTimeService;

        public OrderRepository(IdentityContext dbContext, IMapper mapper,
            IDapper dapper, IDateTimeService dateTimeService) : base(dbContext, mapper)
        {
            _orders = dbContext.Set<Order>();
            context = dbContext;
            this.dapper = dapper;
            this.dateTimeService = dateTimeService;
        }

        public async Task<IEnumerable<GetAllOrderAccessoriesViewModel>> GetAllOrderAccessories(string fromDate, string toDate)
        {
            IEnumerable<GetAllOrderAccessoriesViewModel> accessories;
            if (!(fromDate is { }) && toDate == null)
                accessories = await GetAsync<OrderAccessory, GetAllOrderAccessoriesViewModel>
                    (includeProperties: "Order.Client,Order.Employee,Accessory");
            else if (fromDate != null && toDate == null)
                accessories = await GetAsync<OrderAccessory, GetAllOrderAccessoriesViewModel>
                    (x => x.RepeatDate >= DateTime.Parse(fromDate), includeProperties: "Order.Client,Order.Employee,Accessory");
            else if (fromDate == null && toDate != null)
                accessories = await GetAsync<OrderAccessory, GetAllOrderAccessoriesViewModel>
                    (x => x.RepeatDate <= DateTime.Parse(toDate), includeProperties: "Order.Client,Order.Employee,Accessory");
            else
                accessories = await GetAsync<OrderAccessory, GetAllOrderAccessoriesViewModel>
                    (x => x.RepeatDate >= DateTime.Parse(fromDate) && x.RepeatDate <= DateTime.Parse(toDate),
                    includeProperties: "Order.Client,Order.Employee,Accessory");

            return accessories;
        }

        public async Task<IEnumerable<GetAllOrderAccessoriesViewModel>> GetDelayedOrderAccessories()
        {
            IEnumerable<GetAllOrderAccessoriesViewModel> accessories;
            accessories = await GetAsync<OrderAccessory, GetAllOrderAccessoriesViewModel>
                    (x => x.RepeatDate <= dateTimeService.NowUtc,
                    includeProperties: "Order.Client,Order.Employee,Accessory");

            return accessories;
        }

        public string DeleteOrder(int id)
        {
            try
            {
                DynamicParameters d = new DynamicParameters();
                d.Add("orderId", id);
                dapper.Execute
                    (@$"delete from OrderAccessories where OrderId=@orderId", d, CommandType.Text);
                dapper.Execute
                    (@$"delete from Orders where Id=@orderId", d, CommandType.Text);
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IEnumerable<OrderAccessory>> GetOrderAccessories(int orderId)
        {
            DynamicParameters d = new DynamicParameters();
            d.Add("orderId", orderId);
            List<OrderAccessory> orderAccessories = await dapper.GetAllAsync<OrderAccessory>
                (@$"select * from OrderAccessories where OrderId=@orderId", d, CommandType.Text);

            foreach (var orderAccessory in orderAccessories)
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("AccessoryId", orderAccessory.AccessoryId);
                orderAccessory.Accessory = dapper.Get<Product>
                (@$"select * from Products where Id=@AccessoryId", p, CommandType.Text);
            }
            return orderAccessories;
        }

        public string CreateOrder(CreateOrderCommand command)
        {
            try
            {
                Client client = context.Clients.Where(x => x.Phonenumber == command.Phonenumber).FirstOrDefault();
                decimal price = context.Products.Where(x => x.Id == command.ProductId).FirstOrDefault().Price;
                if (client == null)
                {
                    client = new Client
                    {
                        FirstName = command.FirstName,
                        LastName = command.LastName,
                        FinCode = command.FinCode,
                        Phonenumber = command.Phonenumber
                    };
                    context.Clients.Add(client);
                    context.SaveChanges();
                }

                int? creditId = null;

                ApplicationUser Employee = context.Users.Where(x => x.Id == command.EmployeeId)?.FirstOrDefault();

                if (command.Term != 0)
                {
                    Credit credit = new Credit
                    {
                        Date = dateTimeService.NowUtc.AddMonths(1),
                        Term = command.Term,
                        InitialPayment = command.InitialPayment,
                        Amount = price,
                        Debt = price-command.InitialPayment
                    };
                    context.Credits.Add(credit);
                    context.SaveChanges();
                    creditId = credit.Id;

                    Transaction transaction = new Transaction
                    {
                        Amount=command.InitialPayment,
                        EmployeeId=command.EmployeeId,
                        CreditId = (int)creditId,
                        Date = dateTimeService.NowUtc,
                        PreviousDebt = price,
                        PresentDebt = price - command.InitialPayment
                    };
                    context.Transactions.Add(transaction);
                    context.SaveChanges();

                    Employee.Balance += command.InitialPayment;
                    context.Users.Update(Employee);
                    context.SaveChanges();
                }
                else
                {
                    Employee.Balance += price;
                    context.Users.Update(Employee);
                    context.SaveChanges();
                }

                decimal bonus = (price * Employee.Percentage) / 100;
                if (bonus < Employee.MinAmount)
                    bonus = Employee.MinAmount;
                else if (bonus > Employee.MaxAmount)
                    bonus = Employee.MaxAmount;

                Order order = new Order
                {
                    ProductId = command.ProductId,
                    Price = price,
                    CreateDate = dateTimeService.NowUtc,
                    EmployeeId = command.EmployeeId,
                    ClientId = client.Id,
                    CreditId = creditId
                };
                context.Orders.Add(order);
                context.SaveChanges();

                OrderBonus orderBonus = new OrderBonus
                {
                    OrderId = order.Id,
                    EmployeeId = command.EmployeeId,
                    Bonus = bonus,
                    BonusPaid = false,
                    Percentage = Employee.Percentage,
                    MinAmount = Employee.MinAmount,
                    MaxAmount = Employee.MaxAmount
                };
                context.OrderBonuses.Add(orderBonus);
                context.SaveChanges();

                WarehouseProduct warehouseProduct = context.WarehouseProducts.
                                                        Where(x => x.WarehouseId == command.warehouseId
                                                        && x.ProductId == command.ProductId)
                                                        .FirstOrDefault();
                DynamicParameters x = new DynamicParameters();
                x.Add("warehouseId", warehouseProduct.WarehouseId);
                x.Add("productId", warehouseProduct.ProductId);
                x.Add("balance", warehouseProduct.Balance - 1);
                dapper.Update<WarehouseProduct>
                (@$"update WarehouseProducts set
                    Balance=@balance 
                    where WarehouseId=@warehouseId and ProductId=@productId",
                    x, CommandType.Text);


                List<ProductAccessory> productAccessories = context.ProductAccessories
                                                        .Where(x => x.ParentId == command.ProductId)
                                                        .Include(x => x.Accessory)
                                                        .ToList();
                foreach (var item in productAccessories)
                {
                    DynamicParameters p = new DynamicParameters();
                    p.Add("orderId", order.Id);
                    p.Add("accessoryId", item.AccessoryId);
                    p.Add("repeatDate", order.CreateDate.AddDays(item.Accessory.RepeatDay));
                    p.Add("lastRepeatDate", "1/1/0001 12:00:00 AM");
                    dapper.Insert<OrderAccessory>
                    (@$"insert into OrderAccessories(orderId,AccessoryId,RepeatDate,LastRepeatDate)
                    values(@orderId,@accessoryId,@repeatDate,@lastRepeatDate)", p, CommandType.Text);
                }


                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public string UpdateOrderAccessory(OrderAccessory entity)
        {
            try
            {
                context.OrderAccessories.Update(entity);
                context.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}