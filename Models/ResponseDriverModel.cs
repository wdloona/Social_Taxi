using CSharp_React.EntityFramework.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_React.Models
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
