using ClothesSalePlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p=>p.Name)
                .IsRequired(true);
            builder.Property(p=>p.SizeId)
                .IsRequired(true);
            builder.Property(p=>p.Color)
                .IsRequired(true);
            builder.Property(p=>p.ProductCount)
                .IsRequired(true);
            builder.Property(p=>p.Price)
                .IsRequired(true);
            builder.Property(p=>p.BrandId)
                .IsRequired(true);
            builder.Property(p=>p.InStock)
                .IsRequired(true);
            builder.Property(p=>p.GenderId)
                .IsRequired(true);
            builder.Property(p=>p.StoreId)
                .IsRequired(true);
            builder.HasData(
                new Product
                {
                    Id=1,
                    Name="T-Shirt Catton",
                    SizeId=1,
                    Color="Black",
                    ProductCount=1,
                    Price=19.99,
                    BrandId=1,
                    InStock=true,
                    GenderId=1,
                    StoreId=1,
                    IsDeleted=false,
                }
                );
        }
    }
}
