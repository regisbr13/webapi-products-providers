﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Interfaces.Repository;
using WebApiProductsProviders.Business.Models;
using WebApiProductsProviders.Data.Context;

namespace WebApiProductsProviders.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MyContext context) : base(context)
        {
        }

        public async Task<List<Product>> FindAll(bool provider, bool category)
        {
            if (provider)
            {
                if (category)
                    return await _context.Products.AsNoTracking().Include(x => x.Provider).Include(x => x.Category).ToListAsync();
                return await _context.Products.AsNoTracking().Include(x => x.Provider).ToListAsync();
            }
            return await FindAll();
        }

        public async Task<Product> FindById(Guid id, bool provider, bool category)
        {
            if (provider)
            {
                if (category)
                    return await _context.Products.AsNoTracking().Include(x => x.Provider).Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
                return await _context.Products.AsNoTracking().Include(x => x.Provider).FirstOrDefaultAsync(x => x.Id == id);
            }
            return await FindById(id);
        }

        public async Task<List<Product>> FindByProvider(Guid providerId)
        {
            return await Search(x => x.ProviderId == providerId);
        }
    }

}
