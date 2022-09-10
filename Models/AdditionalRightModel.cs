using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Taxi.Models
{
    public class AdditionalRightModel
    {
        public int AdditionalRidhtId { get; set; }

        public int UserId { get; set; }

        public int RightId { get; set; }

        public string RightName { get; set; }

        public string RightLabel { get; set; }

        public string RightDescription { get; set; }

        public bool HasRight { get; set; }
    }
}
