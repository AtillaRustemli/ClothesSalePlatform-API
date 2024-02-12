using ClothesSalePlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations
{
    public class StoreCanfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.Property(s => s.Name).IsRequired(true);
            builder.Property(s => s.Location).IsRequired(true);
            builder.Property(s => s.Description).IsRequired(true);
            builder.Property(s => s.OpeningHours).IsRequired(true);
            builder.Property(s => s.ClosingHours).IsRequired(true);
            builder.HasData(
                new Store
                {
                    Id = 1,
                    Name = "Atilla's store",
                    Location = "Genclik mts",
                    Description = "Wish you all of the best",
                    OpeningHours = "8:00 AM",
                    ClosingHours = "9:00 PM",
                    IsDeleted = false
                }
                ); ;
        }
    }
}
