using DA.Booking.ApplicationService.BookingModule.Abstracts;
using DA.Booking.ApplicationService.Common;
using DA.Booking.Domain;
using DA.Booking.Dtos.TicketModule;
using DA.Booking.Infrastucture;
using DA.Shared.ApplicationService.Auth;
using DA.Shared.ApplicationService.Vehicle;
using DA.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DA.Booking.ApplicationService.BookingModule.Implements
{
    public class TicketService : BookingServiceBase, ITicketService
    {
        private readonly ILogger _logger;
        private readonly BookingDbContext _dbContext;
        private readonly ICustomerService _customerService;
        private readonly IBusRideService _busRideService;

        public TicketService(ILogger<TicketService> logger, BookingDbContext dbContext, IBusRideService busRideService, ICustomerService customerService) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _customerService = customerService;
            _busRideService = busRideService;
        }

        public async Task<TicketDto> GetTicketByIdAsync(int ticketId)
        {
            var ticket = await _dbContext.Tickets
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null)
            {
                _logger.LogWarning("No ticket found ");
                throw new UserFriendlyException("ticket not found!");
            }
            return new TicketDto
            {
                Id = ticket.Id,
                CustomerName = ticket.CustomerName,
                Route = ticket.Route,
                SeatPosition = ticket.SeatPosition,
                Amount = ticket.Amount,
                Status = ticket.Status,
                CreatedAt = ticket.CreatedAt,
            };
        }

        public async Task<List<TicketDto>> GetTicketsByStatusAsync(int status)
        {
            var tickets = await _dbContext.Tickets
                .Where(ticket => ticket.Status == status)
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    CustomerName = t.CustomerName,
                    Route = t.Route,
                    SeatPosition = t.SeatPosition,
                    Amount = t.Amount,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                })
                .ToListAsync();


            if (tickets.Count == 0)
            {
                _logger.LogWarning("No ticket found ");
                throw new UserFriendlyException("ticket not found!");
            }
            return tickets; 
        }

        public async Task<List<TicketDto>> GetTicketsByCustomerAsync()
        {
            var customer = _customerService.GetCustomer();

            if (customer == null)
            {
                throw new UserFriendlyException("Customer not found.");
            }
            var tickets = await _dbContext.Tickets
                .Where(ticket => ticket.CustomerName == customer.FullName)
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    CustomerName = t.CustomerName,
                    Route = t.Route,
                    SeatPosition = t.SeatPosition,
                    Amount = t.Amount,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                })
                .ToListAsync();


            if (tickets.Count == 0)
            {
                _logger.LogWarning("No ticket found ");
                throw new UserFriendlyException("ticket not found!");
            }
            return tickets;
        }

        public async Task<List<TicketDto>> GetTicketsByBusAsync(int busRideId)
        {
            var bus = _busRideService.GetBusRideByIdAsync(busRideId);
            if (bus == null)
            {
                throw new UserFriendlyException("Bus not found.");
            }

            var tickets = await _dbContext.Tickets
                .Where(ticket => ticket.Route == bus.Result.RideName)
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    CustomerName = t.CustomerName,
                    Route = t.Route,
                    SeatPosition = t.SeatPosition,
                    Amount = t.Amount,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                })
                .ToListAsync();


            if (tickets.Count == 0)
            {
                _logger.LogWarning("No ticket found ");
                throw new UserFriendlyException("ticket not found!");
            }
            return tickets;
        }

        public async Task UpdateTicketStatusAsync(UpdateTicketStatusDto input)
        {
            var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(t => t.Id == input.TicketId);
            if (ticket == null)
            {
                    _logger.LogWarning("No ticket found ");
                    throw new UserFriendlyException("ticket not found!");
            }

            ticket.Status = input.Status;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int ticketId)
        {
            var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticket == null)
            {
                _logger.LogWarning("No ticket found ");
                throw new UserFriendlyException("ticket not found!");
            }

            _dbContext.Tickets.Remove(ticket);
            await _dbContext.SaveChangesAsync();
        }
    }
}
