using ClothesSalePlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(m => m.Name)
                .IsRequired(true);
            builder.Property(m => m.FoundedCountry)
                .IsRequired(true);
            builder.Property(m => m.FoundedYear)
                .IsRequired(true);
            builder.Property(m => m.Description)
                .IsRequired(true);
            builder.HasData(
                new Brand
                {
                    Id=1,
                    Description="Best for your choice",
                    Name="Nike",
                    FoundedYear=2000,
                    FoundedCountry="USA",
                    IsDeleted=false
                }
                
                );
        }
    }
}
