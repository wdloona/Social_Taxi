using CSharp_React.Authorization;
using CSharp_React.Servicies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using WebPush;

namespace ArmMaster.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly NotificationSubscribeService _notificationSubscribeService;

        public NotificationController(NotificationSubscribeService notificationSubscribeService)
        {
            _notificationSubscribeService = notificationSubscribeService;
        }

        [HttpPost("subscribe")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Subscribe([FromBody] PushSubscription sub)
        {
            await _notificationSubscribeService.SubscribeUser(AuthorizationHelper.GetUser(User).UserId, sub);
        }

        [HttpPost("unsubscribe")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Unsubscribe([FromBody] PushSubscription sub)
        {
            await _notificationSubscribeService.UnsubscribeUser(AuthorizationHelper.GetUser(User).UserId, sub);
        }
    }
}