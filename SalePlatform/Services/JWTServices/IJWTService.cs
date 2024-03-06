using ClothesSalePlatform.Models;

namespace ClothesSalePlatform.Services.JWTServices
{
    public interface IJWTService
    {
        string JWTToken(IConfiguration _config, AppUser user,IList<string> roles);

    }
}
