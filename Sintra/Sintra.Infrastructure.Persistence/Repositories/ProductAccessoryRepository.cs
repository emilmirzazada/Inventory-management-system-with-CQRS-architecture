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
    public class ProductAccessoryRepository : GenericRepositoryAsync<ProductAccessory>, IProductAccessoryRepository
    {
        private readonly DbSet<ProductAccessory> ProductAccessories;
        private readonly IDapper dapper;
        private readonly IdentityContext ctx;

        public ProductAccessoryRepository(IdentityContext dbContext, IDapper dapper,
            IdentityContext ctx) : base(dbContext)
        {
            ProductAccessories = dbContext.Set<ProductAccessory>();
            this.dapper = dapper;
            this.ctx = ctx;
        }

        public async Task<List<ProductAccessory>> GetAllProductAccessoriesByProductId(int id)
        {
            DynamicParameters d = new DynamicParameters();
            d.Add("Id", id);
            List<ProductAccessory> ProductAccessorys = await dapper.GetAllAsync<ProductAccessory>
                (@$"select * from ProductAccessories where ParentId=@Id", d, CommandType.Text);

            foreach (var productAccessory in ProductAccessorys)
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("ParentId", productAccessory.ParentId);
                p.Add("AccessoryId", productAccessory.AccessoryId);
                productAccessory.Parent = dapper.Get<Product>
                (@$"select * from Products where Id=@ParentId", p, CommandType.Text);
                productAccessory.Accessory = dapper.Get<Product>
                (@$"select * from Products where Id=@AccessoryId", p, CommandType.Text);
            }

            return ProductAccessorys;
        }

        public ProductAccessory GetProductAccessory(int id, int accessoryId)
        {
            DynamicParameters d = new DynamicParameters();
            d.Add("ParentId", id);
            d.Add("AccessoryId", accessoryId);
            ProductAccessory ProductAccessory = dapper.Get<ProductAccessory>
                (@$"select * from ProductAccessories where ParentId=@ParentId
                    and AccessoryId=@AccessoryId", d, CommandType.Text);

            return ProductAccessory;
        }

        public void DeleteProductAccessory(int id, int accessoryId)
        {
            DynamicParameters d = new DynamicParameters();
            d.Add("ParentId", id);
            d.Add("AccessoryId", accessoryId);
            dapper.Execute
                (@$"delete from ProductAccessories where ParentId=@ParentId
                    and AccessoryId=@AccessoryId", d, CommandType.Text);

        }

        public string AddProductAccessory(int parentId, int accessoryId)
        {
            try
            {
                ProductAccessory ProductAccessory = GetProductAccessory(parentId, accessoryId);
                DynamicParameters p = new DynamicParameters();
                p.Add("parentId", parentId);
                p.Add("accessoryId", accessoryId);
                if (ProductAccessory == null)
                    dapper.Insert<ProductAccessory>
                    (@$"insert into ProductAccessories(ParentId,AccessoryId)
                    values(@parentId,@accessoryId)", p, CommandType.Text);
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
