using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface IProductRepositoryAsync : IGenericRepositoryAsync<Product>
    {
        Task CreateProduct(Product product, int categoryId);
        Task<Product> GetProductById(int id, params Expression<Func<Product, object>>[] includes);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<bool> IsUniqueBarcodeAsync(string barcode);
    }
}
