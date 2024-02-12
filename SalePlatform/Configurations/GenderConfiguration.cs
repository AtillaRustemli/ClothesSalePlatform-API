using ClothesSalePlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.Property(g => g.Name)
                .IsRequired(true);
            builder.HasData(
                new Gender
                {
                    Id = 1,
                    Name = "Men",
                    IsDeleted = false,
                },
                new Gender
                {
                    Id = 2,
                    Name = "Women",
                    IsDeleted = false,
                },
                new Gender
                {
                    Id =3,
                    Name = "UniSex",
                    IsDeleted = false,
                }
                );

        }
    }
}
