﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_React.Models
{
    public class RideModel
    {
        public int RideId { get; set; }
        public int CreatorUserId { get; set; }
        public AddressModel Address { get; set; }
        public int SeatsCount { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool FlActive { get; set; }
        public bool FlFinished { get; set; }
        public string Description { get; set; }
        public UserModel Driver { get; set; }

    }
}
