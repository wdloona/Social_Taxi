using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_React.EntityFramework.Tables
{
    public class Ride
    {
        public int RideId { get; set; }
        public int CreatorUserId { get; set; }
        public string StartPoint { get; set; }
        public string StartCity { get; set; }
        public string StartStreet { get; set; }
        public string StartHome { get; set; }
        public string EndPoint { get; set; }
        public string EndCity { get; set; }
        public string EndStreet { get; set; }
        public string EndHome { get; set; }
        public int SeatsCount { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool FlActive { get; set; }
        public bool FlFinished { get; set; }
        public bool? IsVerifyByAdmin { get; set; }
        public string Description { get; set; }
        public Response Response { get; set; }
    }
}
