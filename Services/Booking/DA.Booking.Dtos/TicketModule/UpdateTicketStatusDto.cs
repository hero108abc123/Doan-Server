using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Booking.Dtos.TicketModule
{
    public class UpdateTicketStatusDto
    {
        public int TicketId { get; set; }
        public int Status { get; set; }
    }
}
