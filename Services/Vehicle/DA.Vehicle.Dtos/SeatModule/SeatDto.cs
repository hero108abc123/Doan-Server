using System;
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
        public string Row { get; set; }
        public string Floor { get; set; }
        public int Status { get; set; }
    }
}
