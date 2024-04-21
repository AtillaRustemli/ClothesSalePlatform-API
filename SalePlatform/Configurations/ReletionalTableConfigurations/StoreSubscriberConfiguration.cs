using ClothesSalePlatform.Models.ReletionTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations.ReletionalTableConfigurations
{
    public class StoreSubscriberConfiguration : IEntityTypeConfiguration<StoreSubscriber>
    {
        public void Configure(EntityTypeBuilder<StoreSubscriber> builder)
        {
            builder
                .Property(s => s.StoreId).IsRequired(true);
            builder
                .Property(s => s.SubscriberId).IsRequired(true);
        }
    }
}
