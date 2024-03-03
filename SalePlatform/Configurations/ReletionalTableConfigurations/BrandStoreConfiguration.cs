using ClothesSalePlatform.Models.ReletionTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations.ReletionalTableConfigurations
{
    public class BrandStoreConfiguration : IEntityTypeConfiguration<BrandStore>
    {
        public void Configure(EntityTypeBuilder<BrandStore> builder)
        {
            builder.Property(bs => bs.BrandId)
                 .IsRequired(true);
            builder.Property(bs=>bs.StoreId)
                .IsRequired(true);
            builder.HasData(
                new BrandStore
                {
                    Id = 1,
                    BrandId = 1,
                    StoreId = 1
                }

                );
        }
    }
}
