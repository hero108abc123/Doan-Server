using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.Dtos.BusRideModule
{
    public class BusRideDto
    {
        public int Id { get; set; }
        public string RideName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public int BusId { get; set; }
        public string BusType { get; set; }
    }
}
