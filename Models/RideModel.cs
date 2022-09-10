using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Taxi.Models
{
    public class RideModel
    {
        public int RideId { get; set; }
        public int CreatorUserId { get; set; }
        public RideSearchParamsModel RideSearchParams { get; set; }
        public int SeatsCount { get; set; }
        public bool FlActive { get; set; }
        public bool FlFinished { get; set; }
        public string Description { get; set; }
        public UserModel Driver { get; set; }

    }
}
