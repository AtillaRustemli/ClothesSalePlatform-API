using Microsoft.EntityFrameworkCore;

namespace ClothesSalePlatform.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options) { }
    }
}
