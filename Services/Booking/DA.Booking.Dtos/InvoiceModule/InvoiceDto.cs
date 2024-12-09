using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Booking.Dtos.InvoiceModule
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string TransactionId { get; set; }
        public int Status { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
