using System;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Models;

namespace WebApiProductsProviders.Business.Interfaces.Repository
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> FindByProvider(Guid providerId);
    }
}
