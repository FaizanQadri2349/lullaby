using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services_lullabay.DTO;
using Services_lullabay.Helper;
using Services_lullabay.IServices;
using Services_lullabay.ViewModel.Account;
using System.IdentityModel.Tokens.Jwt;

namespace Lullabay_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;

        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }
       [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _accountServices.Register(model);

           
            return Ok(result);

        }
        [HttpPost("Login")]
        public async Task<IActionResult> login([FromBody] loginModel model)
        {
            var data = await _accountServices.login(model);
            
            return Ok(data);

        }


    }
}
