using ClothesSalePlatform.Data;
using ClothesSalePlatform.Mapper;
using ClothesSalePlatform.Models;
using ClothesSalePlatform.Services.ProductServices;
using ClothesSalePlatform.Validators.ProductValidators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClothesSalePlatform
{
    public static class RegisterService
    {
        public static void Register(this IServiceCollection services,IConfiguration config)
        {
            services.AddControllers().AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<CreateProductDtoValidator>());

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MapperProfile());
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);

                opt.Password.RequiredLength=8;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;

            }).AddEntityFrameworkStores<AppDbContext>();



            services.AddScoped<IProductService, ProductService>();


        }
    }
}
