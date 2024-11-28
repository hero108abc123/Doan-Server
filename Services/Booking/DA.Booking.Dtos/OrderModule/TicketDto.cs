using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Booking.Dtos.OrderModule
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Route { get; set; }
        public string SeatPosition { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
