using DA.Booking.Dtos.TicketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Booking.ApplicationService.BookingModule.Abstracts
{
    public interface ITicketService
    {
        Task<TicketDto> GetTicketByIdAsync(int ticketId);
        Task<List<TicketDto>> GetTicketsByStatusAsync(int status);
        Task<List<TicketDto>> GetTicketsByCustomerAsync();
        Task<List<TicketDto>> GetTicketsByBusAsync(int busRideId);
        Task UpdateTicketStatusAsync(UpdateTicketStatusDto input);
        Task DeleteTicketAsync(int ticketId);
    }
}
