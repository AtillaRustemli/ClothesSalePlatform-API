using ClothesSalePlatform.Models.ReletionTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations.ReletionalTableConfigurations
{
    public class BrandCategoryConfiguration : IEntityTypeConfiguration<BrandCategory>
    {
        public void Configure(EntityTypeBuilder<BrandCategory> builder)
        {
            builder.Property(bc => bc.CategoryId)
                .IsRequired(true);
            builder.Property(bc => bc.CategoryId)
                .IsRequired(true);


            builder.HasData(
                new BrandCategory
                {
                    Id=1,
                    BrandId = 1,
                    CategoryId = 1,
                }
                );
        }
    }
}
