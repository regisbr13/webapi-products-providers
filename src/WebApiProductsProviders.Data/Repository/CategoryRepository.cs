using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApiProductsProviders.Business.Interfaces.Repository;
using WebApiProductsProviders.Business.Models;
using WebApiProductsProviders.Data.Context;

namespace WebApiProductsProviders.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyContext context) : base(context)
        {
        }

        public async Task<Category> FindById(Guid id, bool products)
        {
            return await _context.Categories.AsNoTracking().Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
