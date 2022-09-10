using CSharp_React.Servicies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharp_React.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly NotificationSubscribeService _notificationSubscribeService;

        public ChatHub(NotificationSubscribeService notificationSubscribeService)
        {
            _notificationSubscribeService = notificationSubscribeService;
        }

        public async Task SendMessageToUser(string userId, string message)
        {
            var userIds = new List<string>() { userId, Context.UserIdentifier };
            await Clients.Users(userIds)?.SendAsync("ReceiveMessage", Context.UserIdentifier, message);

            userIds.Remove(Context.UserIdentifier);
            await _notificationSubscribeService.NotifyUsers(userIds, new Models.NotificationMessageModel()
            {
                Title = "Новое сообщение!",
                Message = message,
                Url = "/second"
            });
        }
    }
}
