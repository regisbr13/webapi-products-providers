using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiProductsProviders.Business.Models;

namespace WebApiProductsProviders.Data.Mappings
{
    public class ProviderMap : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.DocumentNumber).HasMaxLength(14).IsRequired();
            builder.HasMany(x => x.Products).WithOne(x => x.Provider).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
