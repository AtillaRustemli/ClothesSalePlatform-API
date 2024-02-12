using ClothesSalePlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name).IsRequired(true);
            builder.HasData(
                new Category
                {
                    Id= 1,
                    Name="T-Shirt",
                    IsDeleted=false,
                }
                );
        }
    }
}
