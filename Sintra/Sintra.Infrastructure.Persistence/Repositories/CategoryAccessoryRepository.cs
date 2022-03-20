using AutoMapper;
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
    public class CategoryAccessoryRepository : GenericRepositoryAsync<CategoryAccessory>, ICategoryAccessoryRepository
    {
        private readonly DbSet<CategoryAccessory> categoryAccessories;
        private readonly IdentityContext context;
        private readonly IDapper dapper;
        private readonly IMapper mapper;

        public CategoryAccessoryRepository(IdentityContext dbContext, IMapper mapper,
            IDapper dapper) : base(dbContext, mapper)
        {
            categoryAccessories = dbContext.Set<CategoryAccessory>();
            context = dbContext;
            this.dapper = dapper;
            this.mapper = mapper;
        }
        public string AddCategoryAccessory(int categoryId, int accessoryId)
        {
            try
            {
                CategoryAccessory categoryAccessory = GetCategoryAccessory(categoryId, accessoryId);
                DynamicParameters p = new DynamicParameters();
                p.Add("CategoryId", categoryId);
                p.Add("accessoryId", accessoryId);
                if (categoryAccessory == null)
                    dapper.Insert<CategoryAccessory>
                    (@$"insert into CategoryAccessories(CategoryId,AccessoryId)
                    values(@CategoryId,@accessoryId)", p, CommandType.Text);
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public CategoryAccessory GetCategoryAccessory(int id, int accessoryId)
        {
            DynamicParameters d = new DynamicParameters();
            d.Add("CategoryId", id);
            d.Add("AccessoryId", accessoryId);
            CategoryAccessory CategoryAccessory = dapper.Get<CategoryAccessory>
                (@$"select * from CategoryAccessories where CategoryId=@CategoryId
                    and AccessoryId=@AccessoryId", d, CommandType.Text);

            return CategoryAccessory;
        }
        public async Task<List<CategoryAccessory>> GetCategoryAccessories(int id)
        {
            DynamicParameters d = new DynamicParameters();
            d.Add("Id", id);
            List<CategoryAccessory> categoryAccessories = await dapper.GetAllAsync<CategoryAccessory>
                (@$"select * from CategoryAccessories where CategoryId=@Id", d, CommandType.Text);

            foreach (var categoryAccessory in categoryAccessories)
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("CategoryId", categoryAccessory.CategoryId);
                p.Add("AccessoryId", categoryAccessory.AccessoryId);
                categoryAccessory.Category = dapper.Get<Category>
                (@$"select * from Categories where Id=@CategoryId", p, CommandType.Text);
                categoryAccessory.Accessory = dapper.Get<Product>
                (@$"select * from Products where Id=@AccessoryId", p, CommandType.Text);
            }

            return categoryAccessories;
        }

        public void DeleteCategoryAccessory(int id, int accessoryId)
        {
            DynamicParameters d = new DynamicParameters();
            d.Add("CategoryId", id);
            d.Add("AccessoryId", accessoryId);
            dapper.Execute
                (@$"delete from CategoryAccessories where CategoryId=@CategoryId
                    and AccessoryId=@AccessoryId", d, CommandType.Text);

        }
    }
}
