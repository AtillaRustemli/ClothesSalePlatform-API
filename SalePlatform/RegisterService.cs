using ClothesSalePlatform.Data;
using ClothesSalePlatform.Mapper;
using Microsoft.EntityFrameworkCore;

namespace ClothesSalePlatform
{
    public static class RegisterService
    {
        public static void Register(this IServiceCollection services,IConfiguration config)
        {
            services.AddControllers();

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
        }
    }
}
