using ClothesSalePlatform.Data;
using ClothesSalePlatform.Mapper;
using ClothesSalePlatform.Models;
using ClothesSalePlatform.Services.JWTServices;
using ClothesSalePlatform.Services.ProductServices;
using ClothesSalePlatform.Validators.ProductValidators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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

            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(config["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });


            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IJWTService, JWTService>();


        }
    }
}
