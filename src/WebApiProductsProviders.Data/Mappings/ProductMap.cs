using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiProductsProviders.Business.Models;

namespace WebApiProductsProviders.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Image).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Value).HasColumnType("decimal(10,2)");
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
            builder.HasOne(x => x.Provider).WithMany(x => x.Products).HasForeignKey(x => x.ProviderId);
        }
    }
}
