using System.Collections.Generic;

namespace CSharp_React.EntityFramework.Tables
{
    public class User
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public bool IsBlocked { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public string UserPatronymic { get; set; }
      
        public int? Age { get; set; }
      
        public string PhoneNumber { get; set; }
      
        public string DriverLicenceNumber { get; set; }
      
        public double? DrivingExperience { get; set; }

        public List<AdditionalRight> AdditionalRights { get; set; }

        public List<NotificationSubscribe> NotificationSubscribes { get; set; }
      
        public List<Response> Response { get; set; }


    }
}
