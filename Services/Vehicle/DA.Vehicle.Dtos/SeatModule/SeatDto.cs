﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.Dtos.SeatModule
{
    public class SeatDto
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public int IsAvailable { get; set; }

        public int BusId { get; set; }
    }
}
