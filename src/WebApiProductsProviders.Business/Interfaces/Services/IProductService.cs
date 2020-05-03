using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Models;

namespace WebApiProductsProviders.Business.Interfaces.Services
{
    public interface IProductService : IDisposable
    {
        Task<List<Product>> FindAll(bool provider, bool category, int page, int pageSize);
        Task<Product> FindById(Guid id, bool provider, bool category);
        Task<List<Product>> FindByProvider(Guid providerId);
        Task<Product> Insert(Product product);
        Task<Product> Update(Product product);
        Task Remove(Guid id);
    }
}
