using ClothesSalePlatform.Models.ReletionTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations.ReletionalTableConfigurations
{
    public class CategorySubscriberConfiguration : IEntityTypeConfiguration<CategorySubscriber>
    {
        public void Configure(EntityTypeBuilder<CategorySubscriber> builder)
        {
            builder.
                Property(cs => cs.CategoryId).IsRequired(true);
            builder
                .Property(cs=>cs.SubscriberId).IsRequired(true);
        }
    }
}
