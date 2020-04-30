using System;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Models;

namespace WebApiProductsProviders.Business.Interfaces.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> FindById(Guid id, bool products);
    }
}
