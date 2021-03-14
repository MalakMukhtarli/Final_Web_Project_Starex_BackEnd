using Buisness.Abstract;
using Entity.Entities;
using Entity.Entities.Balancess;
using Entity.Entities.Branches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public static int UserNameCode = 15200;
        //public AppUser UserObject;

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
        // POST api/<AuthenticateController>/2
        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<AppUser> appUsers = await _userManager.Users.ToListAsync();
            List<User> users = new List<User>();
            foreach (AppUser appUser in appUsers)
            {
                User user = await GetUserAsync(appUser);
                users.Add(user);
            }
            return Ok(users);
        }

        // POST api/<AuthenticateController>/2
        /// <summary>
        /// Get a User according to 'Id'
        /// </summary>
        /// <param name="id">whichever item you want, you should write its 'Id'</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null) return StatusCode(StatusCodes.Status404NotFound);
            User user = await GetUserAsync(appUser);

            return Ok(user);
        }

        // POST api/<AuthenticateController>/Register
        /// <summary>
        /// Availability of Users Register
        /// </summary>
        /// <param name="register"> Register information </param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            AppUser isExistEmail = await _userManager.FindByEmailAsync(register.Email);
            if (isExistEmail != null) return StatusCode(StatusCodes.Status403Forbidden);
            Branch branchDb = await _contextBranch.GetWithId(register.BranchId);
            if (branchDb == null) return StatusCode(StatusCodes.Status404NotFound);

            Balance balance = new Balance
            {
                Price = register.Balance.Price,
                Currency = register.Balance.Currency,
                MyBalance = register.Balance.MyBalance
            };
            await _contextBalance.Add(balance);
            UserNameCode += 10;
            AppUser newUser = new AppUser
            {
                Name = register.Name,
                Surname = register.Surname,
                Email = register.Email,
                PhoneNumber = register.Phone,
                Gender = register.Gender,
                Birthday = register.Birthday,
                Address = register.Address,
                PassportId = register.PassportId,
                FinCode = register.FinCode,
                IsDeleted = false,
                UserName = $"{UserNameCode}",
                BranchId = register.BranchId,
                BalanceId = balance.Id
            };
            
            IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);
            if (!result.Succeeded) return StatusCode(StatusCodes.Status403Forbidden);
            await _userManager.AddToRoleAsync(newUser, Roles.Admin.ToString());
            return Ok();
        }

        // POST api/<AuthenticateController>/Login
        /// <summary>
        /// Availability of User Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(login.Email);

            if (appUser != null && await _userManager.CheckPasswordAsync(appUser, login.Password))
            {
                JwtSecurityToken token = await SecurityToken(login);

                User user = await GetUserAsync(appUser);

                var newToken = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expirationDate = token.ValidTo,
                    user=user
                };
                return Ok(newToken);
            }

            return Unauthorized();
        }

        // POST api/<AuthenticateController>/Activated
        /// <summary>
        /// whether Users are Active or Inactive
        /// </summary>
        /// <param name="id">whichever item you want, you should write its 'Id'</param>
        /// <returns></returns>
        [HttpPut]
        [Route("activated")]
        public async Task<IActionResult> Activated(string id)
        {
            if (id == null) return StatusCode(StatusCodes.Status404NotFound);
            AppUser appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null) return StatusCode(StatusCodes.Status404NotFound);

            if (appUser.IsDeleted)
            {
                appUser.IsDeleted = false;
            }
            else
            {
                appUser.IsDeleted = true;
            }
            await _userManager.UpdateAsync(appUser);
            return Ok();
        }

        // POST api/<AuthenticateController>/UpdateUser
        /// <summary>
        /// Changing User Data
        /// </summary>
        /// <param name="id">whichever item you want, you should write its 'Id'</param>
        /// <param name="checkUser"> User Data </param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(string id, CheckUserAbout checkUser)
        {
            if (id == null) return StatusCode(StatusCodes.Status404NotFound);

            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null) return StatusCode(StatusCodes.Status404NotFound);

            AppUser isExistEmail = await _userManager.FindByEmailAsync(checkUser.Email);
            if(appUser.Id != isExistEmail.Id)
            {
                if (isExistEmail != null) return StatusCode(StatusCodes.Status403Forbidden);
                appUser.Email = checkUser.Email;
            }

            Branch branchDb = await _contextBranch.GetWithId(checkUser.BranchId);
            if (branchDb == null) return StatusCode(StatusCodes.Status404NotFound);
            appUser.BranchId = checkUser.BranchId;

            appUser.Name = checkUser.Name;
            appUser.Surname = checkUser.Surname;
            appUser.Address = checkUser.Address;
            appUser.Birthday = checkUser.Birthday;
            appUser.FinCode = checkUser.FinCode;
            appUser.Gender = checkUser.Gender;
            appUser.PassportId = checkUser.PassportId;
            appUser.PhoneNumber = checkUser.Phone;

            bool chechPass = await _userManager.CheckPasswordAsync(appUser, checkUser.OldPassword);
            if (!chechPass) return StatusCode(StatusCodes.Status403Forbidden);
            IdentityResult identityResult = await _userManager.ChangePasswordAsync(appUser, checkUser.OldPassword, checkUser.Password);
            if (!identityResult.Succeeded) return StatusCode(StatusCodes.Status403Forbidden);
            if (checkUser.OldPassword == checkUser.Password) return StatusCode(StatusCodes.Status403Forbidden);
            string passToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            await _userManager.ResetPasswordAsync(appUser, passToken, checkUser.Password);

            await _userManager.UpdateAsync(appUser);
            return Ok();
        }

        // POST api/<AuthenticateController>/ChangeRole
        /// <summary>
        /// Changing the User Role
        /// </summary>
        /// <param name="id">whichever item you want, you should write its 'Id'</param>
        /// <param name="role"> New Role of the User </param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeRole")]
        public async Task<IActionResult> ChangeRole(string id, string role)
        {
            if (id == null || role == null) return StatusCode(StatusCodes.Status404NotFound);
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null) return StatusCode(StatusCodes.Status404NotFound);

            IdentityResult addResult = await _userManager.AddToRoleAsync(appUser, role);
            if (!addResult.Succeeded) return StatusCode(StatusCodes.Status403Forbidden);

            string oldRole = (await _userManager.GetRolesAsync(appUser))[0];
            IdentityResult removeResult = await _userManager.RemoveFromRoleAsync(appUser, oldRole);
            if (!removeResult.Succeeded) return StatusCode(StatusCodes.Status403Forbidden);

            await _userManager.AddToRoleAsync(appUser, role);
            return Ok();
        }

        #region Special Actions
        private async Task<User> GetUserAsync(AppUser appUser)
        {
            List<string> roles = new List<string>();
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                roles.Add(role.ToString());
            }
            User user = new User
            {
                Id = appUser.Id,
                Name = appUser.Name,
                Surname = appUser.Surname,
                Email = appUser.Email,
                Phone = appUser.PhoneNumber,
                Address = appUser.Address,
                Birthday = appUser.Birthday,
                FinCode = appUser.FinCode,
                Gender = appUser.Gender,
                PassportId = appUser.PassportId,
                BalanceId = appUser.BalanceId,
                BranchId = appUser.BranchId,
                IsDeleted = appUser.IsDeleted,
                Role = (await _userManager.GetRolesAsync(appUser))[0],
                Roles = roles
            };
            return user;
        }

        private async Task<JwtSecurityToken> SecurityToken(Login login)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(login.Email);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, appUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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
            return token;
        }

        #endregion

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
    }
}
