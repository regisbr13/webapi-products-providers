using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Models;

namespace WebApiProductsProviders.Business.Interfaces.Services
{
    public interface IProviderService : IDisposable
    {
        Task<List<Provider>> FindAll(bool address);
        Task<Provider> FindById(Guid id, bool address, bool product);
        Task Insert(Provider provider);
        Task Update(Provider provider);
        Task UpdateAddress(Address address);
        Task Remove(Guid id);
    }
}
