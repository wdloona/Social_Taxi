using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_React.EntityFramework.Tables
{
    public class Response
    {
        public int ResponseDriverId { get; set; }
        public int RideId { get; set; }
        public int ResponseUserId { get; set; }
        /// <summary>
        /// флаг принятия заявки
        /// </summary>
        public bool? Verification { get; set; }
        public Ride Ride { get; set; }
        public User User { get; set; }
    }
}
