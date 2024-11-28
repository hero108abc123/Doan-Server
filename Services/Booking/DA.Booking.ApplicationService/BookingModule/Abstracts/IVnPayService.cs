using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Booking.ApplicationService.BookingModule.Abstracts
{
    public interface IVnPayService
    {
        string GeneratePaymentLink(decimal amount, string orderInfo);
        string CheckPaymentStatus(string ticketId);
    }
}
