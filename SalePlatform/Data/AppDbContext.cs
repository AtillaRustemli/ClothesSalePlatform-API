using ClothesSalePlatform.Models;
using ClothesSalePlatform.Models.ReletionTables;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ClothesSalePlatform.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options):base(options) { }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Bio> Bio { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<BrandStore> BrandStore { get; set; }
        public DbSet<BrandCategory> BrandCategory { get; set; }
        public DbSet<StoreCategory> StoreCategory { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<BrandSubscriber> BrandSubscriber { get; set; }
        public DbSet<CategorySubscriber> CategorySubscriber { get; set; }
        public DbSet<StoreSubscriber> StoreSubscriber { get; set; }
        public DbSet<ProductSize> ProductSize { get; set; }


        public override int SaveChanges()
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Deleted:
                        data.Entity.DeletedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        data.Entity.UpdatedAt = DateTime.Now;
                        break;
                    case EntityState.Added:
                        data.Entity.CreatedAt = DateTime.Now;
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());

            builder.Entity<Product>()
          .HasOne(p => p.Brand)          
          .WithMany(b => b.Products)     
          .HasForeignKey(p => p.BrandId) 
          .IsRequired();
            builder.Entity<Brand>()
          .HasMany(p => p.Products)          
          .WithOne(b => b.Brand)     
          .HasForeignKey(p => p.BrandId) 
          .IsRequired();


            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(builder);
        }

    }
}
