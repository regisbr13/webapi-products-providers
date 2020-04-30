using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Interfaces.Repository;
using WebApiProductsProviders.Business.Models;
using WebApiProductsProviders.Data.Context;

namespace WebApiProductsProviders.Data.Repository
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(MyContext context) : base(context)
        {
        }

        public async Task<List<Provider>> FindAll(bool address)
        {
            if (address)
            {
                return await _context.Providers.AsNoTracking().Include(x => x.Address).ToListAsync();
            }
            return await FindAll();
        }

        public async Task<Provider> FindById(Guid id, bool address, bool product)
        {
            if (address)
            {
                if (product)
                    return await _context.Providers.AsNoTracking().Include(x => x.Address).Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
                return await _context.Providers.AsNoTracking().Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
            }
            return await FindById(id);
        }
    }
}
