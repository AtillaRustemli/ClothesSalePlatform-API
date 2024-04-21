using ClothesSalePlatform.Models.ReletionTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations.ReletionalTableConfigurations
{
    public class BrandSubscriberConfiguration : IEntityTypeConfiguration<BrandSubscriber>
    {
        public void Configure(EntityTypeBuilder<BrandSubscriber> builder)
        {
            builder
                .Property(bs=>bs.SubscriberId).IsRequired(true);
            builder
                .Property(bs=>bs.BrandId).IsRequired(true);
        }
    }
}
