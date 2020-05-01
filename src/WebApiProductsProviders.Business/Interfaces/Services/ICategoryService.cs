using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Models;

namespace WebApiProductsProviders.Business.Interfaces.Services
{
    public interface ICategoryService : IDisposable
    {
        Task<List<Category>> FindAll();
        Task<Category> FindById(Guid id, bool products);
        Task<Category> Insert(Category category);
        Task<Category> Update(Category category);
        Task Remove(Guid id);
    }
}
