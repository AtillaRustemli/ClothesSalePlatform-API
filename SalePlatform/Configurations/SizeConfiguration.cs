using ClothesSalePlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.Property(s => s.Name).IsRequired(true);
            builder.HasData(
                new Size
                {
                    Id=1,
                    Name="XS",
                    IsDeleted=false
                },
                new Size
                {
                    Id=2,
                    Name="X",
                    IsDeleted = false
                },
                new Size
                {
                    Id=3,
                    Name="M",
                    IsDeleted = false
                },
                new Size
                {
                    Id=4,
                    Name="L",
                    IsDeleted = false
                },
                new Size
                {
                    Id=5,
                    Name="XL",
                    IsDeleted = false
                },
                new Size
                {
                    Id=6,
                    Name="XXL",
                    IsDeleted = false
                },
                new Size
                {
                    Id=7,
                    Name="XXXL",
                    IsDeleted = false
                }
                );
        }
    }
}
