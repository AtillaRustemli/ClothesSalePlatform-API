using ClothesSalePlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(pi => pi.ImgUrl).IsRequired(true);
            builder.Property(pi => pi.ProductId).IsRequired(true);
            builder.HasData(
                new ProductImage
                {
                    Id = 1,
                    ImgUrl = "yellow-catton-tshirt.png",
                    ProductId = 1
                }
                );
        }
    }
}
