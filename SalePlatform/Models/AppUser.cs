using Microsoft.AspNetCore.Identity;

namespace ClothesSalePlatform.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
