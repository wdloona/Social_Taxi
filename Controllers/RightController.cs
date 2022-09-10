using Social_Taxi.Servicies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Social_Taxi.Authorization;

namespace Social_Taxi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RightController : ControllerBase
    {
        private readonly RightService _rightService;

        public RightController(RightService rightService)
        {
            _rightService = rightService;
        }

        [HttpGet]
        public async Task<bool> CheckRight([FromQuery] string componentName)
        {
            var user = AuthorizationHelper.GetUser(User);
            return await _rightService.HasComponentRightAccess(user.UserId, componentName.Replace("Component", ""), "Read");
        }

        [HttpGet("Edit")]
        public async Task<bool> CheckEditRight([FromQuery] string componentName)
        {
            var user = AuthorizationHelper.GetUser(User);
            return await _rightService.HasComponentRightAccess(user.UserId, componentName.Replace("Component", ""), "Edit");
        }

        [HttpGet("Create")]
        public async Task<bool> CheckCreateRight([FromQuery] string componentName)
        {
            var user = AuthorizationHelper.GetUser(User);
            return await _rightService.HasComponentRightAccess(user.UserId, componentName.Replace("Component", "").Replace("New",""), "Create");
        }
    }
}
