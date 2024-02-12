using ClothesSalePlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations
{
    public class BioConfiguration : IEntityTypeConfiguration<Bio>
    {
        public void Configure(EntityTypeBuilder<Bio> builder)
        {
            builder.Property(b => b.Key).IsRequired(true);
            builder.Property(b => b.Value).IsRequired(true);
            builder.HasData(
                new Bio
                {
                    Id= 1,
                    Key="Number",
                    Value="050-624-54-05",
                    IsDeleted=false,
                }
                );

        }
    }
}
