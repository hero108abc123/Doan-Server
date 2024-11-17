using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.Dtos.BusModule
{
    public class BusDto
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public decimal Price { get; set; }
        public int TotalSeat { get; set; }
    }
}
