using System.Collections.Generic;

namespace CSharp_React.EntityFramework.Tables
{
    public class Right
    {
        public int RightId { get; set; }

        public string RightName { get; set; }

        public string RightLabel { get; set; }

        public string RightDescription { get; set; }

        public List<Role> Roles { get; set; }

        public List<AdditionalRight> AdditionalRights { get; set; }
    }
}
