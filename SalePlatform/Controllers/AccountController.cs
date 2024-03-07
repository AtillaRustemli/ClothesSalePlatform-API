using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.AccountDTOs;
using ClothesSalePlatform.Models;
using ClothesSalePlatform.Services.JWTServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IJWTService _jwtService;


        public AccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration config, IJWTService jwtService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _config = config;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult GetAll(string? search=null)
        {
            var users = _userManager.Users.ToList();
            if (!string.IsNullOrEmpty(search))
            {
               users=users.Where(u=>u.UserName.ToLower().Contains(search.ToLower())).ToList();
            }
            ReturnUserListDto returnUserListDto = new()
            {
                UserCount=users.Count
            };

           returnUserListDto.Values=_mapper.Map<List<ReturnUserDto>>(users);
            return Ok(returnUserListDto);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Regiter(RegisterDto registerDto)
        {
            if (registerDto == null)  return BadRequest();
            var user=await _userManager.FindByNameAsync(registerDto.UserName);
            var userEmail=await _userManager.FindByEmailAsync(registerDto.Email);
            if (user != null||userEmail!=null) return BadRequest();
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
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
                if(user== null) return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password,true,true);
            if (result.IsLockedOut) return BadRequest();
            if (!result.Succeeded) return BadRequest(result);

            var roles=await _userManager.GetRolesAsync(user);

            var stringToken = _jwtService.JWTToken(_config,user,roles);




            return Ok(new {Message="Signed in Succesifuly",token= stringToken });
        }



    }
}
