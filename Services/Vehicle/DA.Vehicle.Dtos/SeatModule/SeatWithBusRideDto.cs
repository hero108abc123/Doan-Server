using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.Dtos.SeatModule
{
    public class SeatWithBusRideDto
    {
        public int SeatId { get; set; }
        public string Position { get; set; }
        public string Row { get; set; }
        public string Floor { get; set; }
        public int Status { get; set; }
        public int BusId { get; set; }
        public decimal Price { get; set; }
        public int BusRideId { get; set; }
        public string BusRideName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
    }
}
