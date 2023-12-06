using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services_lullabay.Helper;
using Services_lullabay.IServices;

namespace Lullabay_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IDeviceServices _Services;

        public AdminController(IDeviceServices Services)
        {
            _Services = Services;
        }
        [Authorize(Roles = UserRoles.Admin)]
       [HttpGet("GetAll")]
        public ActionResult getall()
        {
            var data = _Services.getall();
            return Ok(data);
        }
        [HttpGet("get by udid")]
        public ActionResult get(string udid)
        {
            var data = _Services.getbyudid(udid);
            return Ok(new { data });
        }
        [HttpGet("GetDashboard")]
        public ActionResult dash() 
        {
            var data=_Services.dashboard();
            return Ok(new { data });
        }
        [HttpGet("GetLocationCount")]
        public ActionResult loc()
        {
            var data=_Services.location();
            return Ok(new { data });
        }
       
    }
}
