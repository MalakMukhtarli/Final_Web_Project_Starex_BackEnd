using Buisness.Abstract;
using Entity.Entities;
using Entity.Entities.Balancess;
using Entity.Entities.Branches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Starex.Extension;
using Starex.ToDoItems;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Starex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IBranchService _contextBranch;
        private readonly IBalanceService _contextBalance;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IConfiguration Configuration { get; }
        public AuthenticateController(UserManager<AppUser> userManager,
                                      RoleManager<IdentityRole> roleManager,
                                      IConfiguration configuration,
                                      IBranchService contextBranch,
                                      IBalanceService contextBalance)
        {
            _contextBranch = contextBranch;
            _contextBalance = contextBalance;
            _userManager = userManager;
            _roleManager = roleManager;
            Configuration = configuration;
        }

        // POST api/<AuthenticateController>/Register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            //AppUser isExistUsername = await _userManager.FindByNameAsync(register.UserName);
            //if (isExistUsername != null) return StatusCode(StatusCodes.Status403Forbidden);
            AppUser isExistEmail = await _userManager.FindByEmailAsync(register.Email);
            if (isExistEmail != null) return StatusCode(StatusCodes.Status403Forbidden);
            Branch branchDb = await _contextBranch.GetWithId(register.BranchId);
            if (branchDb == null) return StatusCode(StatusCodes.Status404NotFound);
            Balance balance = new Balance
            {
                Price = 0,
                Currency = null,
                MyBalance = 0
            };
            await _contextBalance.Add(balance);

            AppUser newUser = new AppUser
            {
                Name = register.Name,
                Surname = register.Surname,
                Email = register.Email,
                PhoneNumber=register.Phone,
                Gender = register.Gender,
                Birthday = register.Birthday,
                Address = register.Address,
                PassportId = register.PassportId,
                FinCode = register.FinCode,
                IsDeleted = false,
                UserName = register.Email,
                BranchId = register.BranchId,
                BalanceId = balance.Id
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);
            if (!result.Succeeded) return StatusCode(StatusCodes.Status403Forbidden);
            await _userManager.AddToRoleAsync(newUser, Roles.Admin.ToString());
            return Ok();
        }

        #region Create User Role
        //[Route("CreateUserRole")]
        //public async Task CreateUserRole()
        //{
        //    if (!(await _roleManager.RoleExistsAsync(Roles.SuperAdmin.ToString())))
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Roles.SuperAdmin.ToString() });
        //    if (!(await _roleManager.RoleExistsAsync(Roles.Admin.ToString())))
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Admin.ToString() });
        //    if (!(await _roleManager.RoleExistsAsync(Roles.SimpleUser.ToString())))
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Roles.SimpleUser.ToString() });
        //}
        #endregion

        // POST api/<AuthenticateController>/Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(login.Email);

            if (appUser != null && await _userManager.CheckPasswordAsync(appUser, login.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,appUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                var userRole = await _userManager.GetRolesAsync(appUser);
                foreach (var role in userRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: Configuration["JWT:ValidIssuer"],
                    audience: Configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(60),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expirationDate = token.ValidTo
                });
            }

            return Unauthorized();
        }

    }
}
