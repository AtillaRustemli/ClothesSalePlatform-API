using ClothesSalePlatform.Models.ReletionTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations.ReletionalTableConfigurations
{
    public class StoreCategoryConfiguration : IEntityTypeConfiguration<StoreCategory>
    {
        public void Configure(EntityTypeBuilder<StoreCategory> builder)
        {
          
            builder.Property(sc => sc.StoreId)
                .IsRequired(true);
            builder.Property(sc => sc.CategoryId)
                .IsRequired(true);

            builder.HasData(
                new StoreCategory
                {
                    Id =1,
                    StoreId = 1,
                    CategoryId = 1
                }


                );
        }
    }
}
