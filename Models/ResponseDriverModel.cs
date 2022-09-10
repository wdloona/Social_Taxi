using Social_Taxi.EntityFramework.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Taxi.Models
{
    public class ResponseDriverModel
    {
        public int ResponseDriverId { get; set; }            
        public int RideId { get; set; }
        public int ResponseUserId { get; set; }
        public bool? VerificationByOwnerRide { get; set; }
        public RideModel RideModel { get; set; }

    }
}
