using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services_lullabay.DTO;
using Services_lullabay.Helper;
using Services_lullabay.IServices;

namespace Lullabay_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceApiController : ControllerBase
    {
        private readonly IDeviceServices _Services;

        public DeviceApiController(IDeviceServices Services)
        {
            _Services = Services;
        }
        [HttpPost]
        public ActionResult<AddDevice> create(AddDevice add)
        {
           
            var responseId= _Services.create(add);
            if(responseId!=null)
            {
                return Ok(new {responseId});
            }
            return BadRequest("Oops! Something went wrong");
        }
        [HttpPost("Update")]
        public ActionResult<UpdateDevice> update(UpdateDevice update) 
        {
          var result=  _Services.update(update);
            var response = new ApiResponse<UpdateDevice>
            {
                Status = "success",
                Data = result,
                Message = "EnddateTime added successfully"
            };
            return Ok(response);
          
        
        }
    }
}
