using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Features.Warehouses.Queries.GetWarehouseById;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using Sintra.Infrastructure.Persistence.Contexts;
using Sintra.Infrastructure.Persistence.Dapper;
using Sintra.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Infrastructure.Persistence.Repositories
{
    public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
    {
        private readonly DbSet<Product> _products;
        private readonly IdentityContext context;
        private readonly IMapper mapper;
        private readonly IDapper dapper;

        public ProductRepositoryAsync(IdentityContext dbContext,IMapper mapper,
            IDapper dapper) : base(dbContext, mapper)
        {
            _products = dbContext.Set<Product>();
            context = dbContext;
            this.mapper = mapper;
            this.dapper = dapper;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await dapper.GetAllAsync<Product>(
                "select * from Products where Deleted=0 order by id desc", null);
            return products;
        }
        public async Task CreateProduct(Product product,int categoryId)
        {
            await AddAsync(product);

            ProductCategory productCategory = new ProductCategory
            {
                ProductId = product.Id,
                CategoryId = categoryId
            };
            context.ProductCategories.Add(productCategory);
            context.SaveChanges();

            if (product.ProductTypeId==1)
            {
                IEnumerable<CategoryAccessory> categoryAccessories = context.CategoryAccessories
                                                                    .Where(x => x.CategoryId == categoryId)
                                                                    .ToList();
                List<ProductAccessory> productAccessories = new List<ProductAccessory>();
                foreach (var categoryAccessory in categoryAccessories)
                {
                    ProductAccessory productAccessory = new ProductAccessory
                    {
                        ParentId = product.Id,
                        AccessoryId = categoryAccessory.AccessoryId
                    };
                    productAccessories.Add(productAccessory);
                }
                context.ProductAccessories.AddRange(productAccessories);
                context.SaveChanges();
            }
        }

        public async Task<Product> GetProductById(int id, params Expression<Func<Product,object>>[] includes)
        {
            var products = context.Products
                                  .Where(x => x.Id == id);
            foreach (var include in includes)
            {
                products = products.Include(include);
            }
            return await products.FirstOrDefaultAsync();
        }
        public Task<bool> IsUniqueBarcodeAsync(string barcode)
        {
            return _products
                .AllAsync(p => p.Barcode != barcode);
        }
    }
}
