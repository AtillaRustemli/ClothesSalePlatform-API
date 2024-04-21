using ClothesSalePlatform.Models.ReletionTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations.ReletionalTableConfigurations
{
    public class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
    {
        public void Configure(EntityTypeBuilder<ProductSize> builder)
        {
            builder
                .Property(ps=>ps.ProductId).IsRequired(true);
            builder
                .Property(ps=>ps.SizeId).IsRequired(true);
            builder.HasData(
                new ProductSize
                {
                    Id=1,
                    ProductId=1,
                    SizeId=1,
                   
                }
                );
        }
    }
}
