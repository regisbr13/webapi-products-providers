using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Models;

namespace WebApiProductsProviders.Business.Interfaces.Repository
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Task<List<Provider>> FindAll(bool address);
        Task<Provider> FindById(Guid id, bool address, bool product);
    }
}
