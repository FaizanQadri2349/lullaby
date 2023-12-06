using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services_lullabay.Authentication;
using Services_lullabay.DTO;
using Services_lullabay.Helper;
using Services_lullabay.IServices;
using Services_lullabay.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services_lullabay.Services
{
    public class AccountServices : IAccountServices
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task <IActionResult> login([FromBody] loginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                return new OkObjectResult(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    User = user.UserName

                });
            }
           
             return new UnauthorizedResult();
        }

        public async Task<string> Register([FromBody] RegisterModel model)
        {
            var userexist = await _userManager.FindByNameAsync(model.UserName);
            if (userexist != null)
            
                 return "user already exists";// StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "error", Message = "user already exists" });
               
            ApplicationUser user = new ApplicationUser()
            {
                
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.password);
            if (!result.Succeeded)
            {
               return "Password should contain upper and lower case numbr and alpha numeric" ;
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            // Add user to the appropriate role
            if (model.IsAdmin)
            {
                // Add user to the "Admin" role
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            else
            {
                // Add user to the "User" role
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

           
            return "created"; 


        }
    }
    
}
