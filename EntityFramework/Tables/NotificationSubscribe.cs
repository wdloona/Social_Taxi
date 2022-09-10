
namespace Social_Taxi.EntityFramework.Tables
{
    public class NotificationSubscribe
    {                      
        public int NotificationSubscribeId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string Auth { get; set; }
        
        public string P256DH { get; set; }

        public string Endpoint { get; set; }
    }
}
