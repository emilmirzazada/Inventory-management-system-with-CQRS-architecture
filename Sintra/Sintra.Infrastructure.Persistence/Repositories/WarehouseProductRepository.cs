using Dapper;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using Sintra.Infrastructure.Persistence.Contexts;
using Sintra.Infrastructure.Persistence.Dapper;
using Sintra.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Infrastructure.Persistence.Repositories
{
    public class WarehouseProductRepository : GenericRepositoryAsync<WarehouseProduct>, IWarehouseProductRepository
    {
        private readonly DbSet<WarehouseProduct> warehouseProducts;
        private readonly IDapper dapper;
        private readonly IdentityContext ctx;

        public WarehouseProductRepository(IdentityContext dbContext, IDapper dapper,
            IdentityContext ctx) : base(dbContext)
        {
            warehouseProducts = dbContext.Set<WarehouseProduct>();
            this.dapper = dapper;
            this.ctx = ctx;
        }

        public async Task<List<WarehouseProduct>> GetWarehousesByProductId(int productId)
        {
            DynamicParameters d = new DynamicParameters();
            d.Add("productId", productId);
            List<WarehouseProduct> warehouseProducts = await dapper.GetAllAsync<WarehouseProduct>
                (@$"select * from WarehouseProducts where ProductId=@productId", d, CommandType.Text);

            foreach (var warehouse in warehouseProducts)
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("WarehouseId", warehouse.WarehouseId);
                warehouse.Warehouse = dapper.Get<Warehouse>
                (@$"select * from Warehouses where Id=@WarehouseId", p, CommandType.Text);
            }

            return warehouseProducts;
        }

        public async Task<List<WarehouseProduct>> GetAllWarehouseProductsByWarehouseId(int id)
        {
            DynamicParameters d = new DynamicParameters();
            d.Add("Id", id);
            List<WarehouseProduct> warehouseProducts = await dapper.GetAllAsync<WarehouseProduct>
                (@$"select * from WarehouseProducts where WarehouseId=@Id", d, CommandType.Text);

            foreach (var warehouse in warehouseProducts)
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("WarehouseId", warehouse.WarehouseId);
                p.Add("ProductId", warehouse.ProductId);
                warehouse.Warehouse = dapper.Get<Warehouse>
                (@$"select * from Warehouses where Id=@WarehouseId", p, CommandType.Text);
                warehouse.Product = dapper.Get<Product>
                (@$"select * from Products where Id=@ProductId", p, CommandType.Text);
            }

            return warehouseProducts;
        }

        public WarehouseProduct GetWarehouseProduct(int id, int productId)
        {
            DynamicParameters d = new DynamicParameters();
            d.Add("WarehouseId", id);
            d.Add("ProductId", productId);
            WarehouseProduct warehouseProduct = dapper.Get<WarehouseProduct>
                (@$"select * from WarehouseProducts where WarehouseId=@WarehouseId
                    and ProductId=@ProductId", d, CommandType.Text);

            return warehouseProduct;
        }

        public void DeleteWarehouseProduct(int id, int productId)
        {
            try
            {
                DynamicParameters d = new DynamicParameters();
                d.Add("WarehouseId", id);
                d.Add("ProductId", productId);
                dapper.Execute
                    (@$"delete from WarehouseProducts where WarehouseId=@WarehouseId
                    and ProductId=@ProductId", d, CommandType.Text);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public string AddWarehouseProduct(int warehouseId, int productId, int balance)
        {
            try
            {
                WarehouseProduct warehouseProduct = GetWarehouseProduct(warehouseId, productId);
                DynamicParameters p = new DynamicParameters();
                p.Add("warehouseId", warehouseId);
                p.Add("productId", productId);
                p.Add("balance", balance);
                if (warehouseProduct == null)
                    dapper.Insert<WarehouseProduct>
                    (@$"insert into WarehouseProducts(WarehouseId,ProductId,Balance)
                    values(@warehouseId,@productId,@balance)", p, CommandType.Text);
                else
                    UpdateWarehouseProduct(warehouseId, productId, balance);
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateWarehouseProduct(int warehouseId, int productId, int balance)
        {
            try
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("warehouseId", warehouseId);
                p.Add("productId", productId);
                p.Add("balance", balance);
                dapper.Update<WarehouseProduct>
                (@$"update WarehouseProducts set
                    Balance=@balance 
                    where WarehouseId=@warehouseId and ProductId=@productId",
                    p, CommandType.Text);
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
