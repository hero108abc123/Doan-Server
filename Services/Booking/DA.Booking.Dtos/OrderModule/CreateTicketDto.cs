using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Booking.Dtos.OrderModule
{
    public class CreateTicketDto
    {
        public int BusRideId { get; set; }
        public int SeatId { get; set; }
    }
}
