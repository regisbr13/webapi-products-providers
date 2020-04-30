using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Interfaces.Repository;
using WebApiProductsProviders.Business.Models;
using WebApiProductsProviders.Data.Context;

namespace WebApiProductsProviders.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(MyContext context) : base(context)
        {
        }

        public async Task<Address> FindByProvider(Guid providerId)
        {
            return await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(x => x.ProviderId == providerId);
        }
    }
}
