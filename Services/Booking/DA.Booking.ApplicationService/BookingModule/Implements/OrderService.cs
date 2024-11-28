using DA.Booking.ApplicationService.BookingModule.Abstracts;
using DA.Booking.ApplicationService.Common;
using DA.Booking.Domain;
using DA.Booking.Dtos.OrderModule;
using DA.Booking.Infrastucture;
using DA.Shared.ApplicationService.Auth;
using DA.Shared.ApplicationService.Vehicle;
using DA.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Booking.ApplicationService.BookingModule.Implements
{
    public class OrderService : BookingServiceBase, IOrderService
    {
        private readonly ILogger _logger;
        private readonly BookingDbContext _dbContext;
        private readonly IBusRideService _busRideService;
        private readonly ICustomerService _customerService;
        private readonly IVnPayService _vnPayService;
        public OrderService(ILogger<OrderService> logger, BookingDbContext dbContext, IBusRideService busRideService, ICustomerService customerService, IVnPayService vnPayService) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _busRideService = busRideService;
            _customerService = customerService;
            _vnPayService = vnPayService;
        }

        public async Task CreateTicketAsync(CreateTicketDto request)
        {
            var seat = _busRideService.GetSeatWithBusRide(request.BusRideId, request.SeatId);

            if (seat == null)
            {
                throw new UserFriendlyException("Seat not found.");
            }

            var customer = _customerService.GetCustomer();
            if (seat == null)
            {
                throw new UserFriendlyException("Customer not found.");
            }

            var ticket = new Ticket
            {
                CustomerName = customer.FullName,
                Route = seat.Result.BusRideName,
                SeatPosition = "{seat.Result.Row}{seat.Result.Position}-{seat.Result.Floor}",
                Amount = seat.Result.Price,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };
            var paymentLink = _vnPayService.GeneratePaymentLink(ticket.Amount, ticket.Id.ToString());
            await _dbContext.Tickets.AddAsync(ticket);
            await _dbContext.SaveChangesAsync();
            
        }

        public async Task ProcessPaymentStatusAsync(int ticketId)
        {
            var ticket = await _dbContext.Tickets.FindAsync(ticketId);

            if (ticket == null)
            {
                throw new Exception("Ticket not found.");
            }

            // 3. Kiểm tra trạng thái thanh toán qua VNPay
            var paymentStatus = _vnPayService.CheckPaymentStatus(ticket.Id.ToString());

            if (paymentStatus == "Paid")
            {
                // 4. Cập nhật trạng thái vé thành 'Confirmed'
                ticket.Status = "Confirmed";

                // 5. Tạo hóa đơn thanh toán
                var invoice = new Invoice
                {
                    TicketId = ticket.Id,
                    TransactionId = Guid.NewGuid().ToString(),
                    Status = "Paid",
                    PaymentDate = DateTime.UtcNow
                };

                await _dbContext.Invoices.AddAsync(invoice);
            }
            else
            {
                // Cập nhật trạng thái vé thành 'Expired' nếu không thanh toán
                ticket.Status = "Expired";
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
