using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothesSalePlatform.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
             new IdentityRole
             {
                 Id = Guid.NewGuid().ToString(),
                 Name = "Admin",
                 NormalizedName = "ADMIN",
                 ConcurrencyStamp = Guid.NewGuid().ToString()

             }); builder.HasData(
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()

            }); builder.HasData(
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Member",
                NormalizedName = "MEMBER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
        }
    }
}
