using System.Collections.Generic;

namespace Social_Taxi.EntityFramework.Tables
{
    public class Role
    {                      
        public int RoleId { get; set; }
                    
        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public List<User> Users { get; set; }

        public List<Right> Rights { get; set; }
    }
}
