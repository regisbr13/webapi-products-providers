using Microsoft.EntityFrameworkCore;
using WebApiProductsProviders.Business.Models;

namespace WebApiProductsProviders.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(MyContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
