using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Taxi.Models
{
    public class RideSearchParamsModel
    {
        public string StartPoint { get; set; }
        public string StartCity { get; set; }
        public string StartStreet { get; set; }
        public string StartHome { get; set; }
        public string EndPoint { get; set; }
        public string EndCity { get; set; }
        public string EndStreet { get; set; }
        public string EndHome { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
