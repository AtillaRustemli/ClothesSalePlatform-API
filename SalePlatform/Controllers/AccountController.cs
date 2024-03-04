using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.AccountDTOs;
using ClothesSalePlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClothesSalePlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Regiter(RegisterDto registerDto)
        {
            if (registerDto == null)  return BadRequest();
            var user=await _userManager.FindByNameAsync(registerDto.UserName);
            if (user != null) return BadRequest();
            user = new()
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
            };
            var role =await _roleManager.FindByNameAsync("Member");
            var result=await _userManager.CreateAsync(user, registerDto.Password);
            await _userManager.AddToRoleAsync(user,role.ToString());
            await _userManager.UpdateAsync(user);
            if(!result.Succeeded) return BadRequest(result.Errors);



            return Ok(201);
        }



    }
}
