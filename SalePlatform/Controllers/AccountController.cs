using AutoMapper;
using ClothesSalePlatform.Data;
using ClothesSalePlatform.DTOs.AccountDTOs;
using ClothesSalePlatform.Models;
using ClothesSalePlatform.Services.EmailServices;
using ClothesSalePlatform.Services.JWTServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
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
        private readonly IEmailService _emailService;


        public AccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration config, IJWTService jwtService, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _config = config;
            _jwtService = jwtService;
            _emailService = emailService;
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
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = Url.Action(nameof(VerifyEmail), "Account", new { email = user.Email, token }, Request.Scheme, Request.Host.ToString());
            _emailService.ConfirmEmail(registerDto.Email, "Confirm With your Email", url);


            return Ok(201);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
                if (!user.EmailConfirmed)
                {
                    return BadRequest("Email tesdiqlenmeyib");
                }
                if(user== null) return BadRequest();
            }
            if(!user.EmailConfirmed)
            {
                return StatusCode(400);
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password,true,true);
            if (result.IsLockedOut) return BadRequest();
            if (!result.Succeeded) return BadRequest(result);

            var roles=await _userManager.GetRolesAsync(user);

            var stringToken = _jwtService.JWTToken(_config,user,roles);




            return Ok(new {Message="Signed in Succesifuly",token= stringToken });
        }

        [Authorize(Roles="Admin")]
        [HttpPost("RoleChange")]
        public async Task<IActionResult> RoleChange(RoleChangeDto roleChange)
        {
            if (roleChange == null) return BadRequest();
            var user =await _userManager.FindByNameAsync(roleChange.UserName);
            if(user == null) return NotFound();
            var userRoles =await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user,userRoles);
            await _userManager.AddToRolesAsync(user, roleChange.Roles);
            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound();
            if (user.EmailConfirmed)
            {
                return Ok(new { result = "success" });
            }
            await _userManager.ConfirmEmailAsync(user, token);
            await _signInManager.SignInAsync(user, true);
            return Ok(new {Email=email,Token=token});   
        }
     

    }
}
