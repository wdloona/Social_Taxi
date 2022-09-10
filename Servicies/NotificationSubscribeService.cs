using Social_Taxi.EntityFramework;
using Social_Taxi.EntityFramework.Tables;
using Social_Taxi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPush;

namespace Social_Taxi.Servicies
{
    public class NotificationSubscribeService
    {
        private readonly TaxiDbContext _taxiDbContext;
        private readonly VapidDetails _vapidDetails;

        public NotificationSubscribeService(TaxiDbContext taxiDbContext, VapidDetails vapidDetails)
        {
            _taxiDbContext = taxiDbContext;
            _vapidDetails = vapidDetails;
        }

        public async Task SubscribeUser(int userId, PushSubscription pushSubscription)
        {
            var notificationSubscribe = await _taxiDbContext.Set<NotificationSubscribe>()
                .FirstOrDefaultAsync(n => n.UserId == userId && n.Auth == pushSubscription.Auth
                    && n.P256DH == pushSubscription.P256DH && n.Endpoint == pushSubscription.Endpoint);

            if (notificationSubscribe == null)
            {
                notificationSubscribe = new NotificationSubscribe()
                {
                    UserId = userId,
                    Auth = pushSubscription.Auth,
                    P256DH = pushSubscription.P256DH,
                    Endpoint = pushSubscription.Endpoint
                };

                await _taxiDbContext.AddAsync(notificationSubscribe);
                await _taxiDbContext.SaveChangesAsync();
            }
        }
        public async Task UnsubscribeUser(int userId, PushSubscription pushSubscription)
        {
            var notificationSubscribes = await _taxiDbContext.Set<NotificationSubscribe>()
                .Where(n => n.UserId == userId && n.Auth == pushSubscription.Auth
                    && n.P256DH == pushSubscription.P256DH && n.Endpoint == pushSubscription.Endpoint).ToListAsync();

            if (notificationSubscribes.Any())
            {
                _taxiDbContext.RemoveRange(notificationSubscribes);
                await _taxiDbContext.SaveChangesAsync();
            }
        }

        public async Task NotifyUsers(List<string> userIds, NotificationMessageModel notificationMessage)
        {
            var client = new WebPushClient();
            var serializedMessage = JsonConvert.SerializeObject(notificationMessage);

            var notificationSubscribes = await _taxiDbContext.Set<NotificationSubscribe>()
                .Where(n => userIds.Contains(n.UserId.ToString())).ToListAsync();

            List<NotificationSubscribe> subscribesToDelete = new();

            foreach (var userSubscription in notificationSubscribes)
            {
                try
                {
                    var pushSubscription = new PushSubscription()
                    {
                        Auth = userSubscription.Auth,
                        P256DH = userSubscription.P256DH,
                        Endpoint = userSubscription.Endpoint
                    };

                    client.SendNotification(pushSubscription, serializedMessage, _vapidDetails);
                }
                catch (WebPushException exception)
                {
                    if (exception.StatusCode == System.Net.HttpStatusCode.Gone)
                    {
                        subscribesToDelete.Add(userSubscription);
                    }

                    Console.WriteLine("Http STATUS code - " + exception.StatusCode);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error: " + exception.Message);
                }
            }

            if (subscribesToDelete.Any())
            {
                _taxiDbContext.RemoveRange(subscribesToDelete);
                await _taxiDbContext.SaveChangesAsync();
            }
        }
    }
}
