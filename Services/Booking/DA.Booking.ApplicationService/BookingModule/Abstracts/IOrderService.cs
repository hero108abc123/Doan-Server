using DA.Booking.Dtos.TicketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Booking.ApplicationService.BookingModule.Abstracts
{
    public interface IOrderService
    {
        Task CreateTicketAsync(CreateTicketDto request);
        Task ProcessPaymentAsync(int ticketId);
    }
}
