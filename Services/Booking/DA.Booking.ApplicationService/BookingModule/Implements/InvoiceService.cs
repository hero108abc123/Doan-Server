using DA.Booking.ApplicationService.BookingModule.Abstracts;
using DA.Booking.ApplicationService.Common;
using DA.Booking.Domain;
using DA.Booking.Dtos.InvoiceModule;
using DA.Booking.Infrastucture;
using DA.Shared.ApplicationService.Auth;
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
    public class InvoiceService : BookingServiceBase, IInvoiceService
    {
        private readonly ILogger _logger;
        private readonly BookingDbContext _dbContext;
        private readonly ICustomerService _customerService;
        public InvoiceService(ILogger<InvoiceService> logger, BookingDbContext dbContext, ICustomerService customerService) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _customerService = customerService;
        }

        public async Task<List<InvoiceDto>> GetInvoicesByCustomerAsync()
        {
            var customer = _customerService.GetCustomer();
            if (customer == null)
            {
                throw new UserFriendlyException("Customer not found.");
            }
            var invoices = await _dbContext.Invoices
                .Include(i=> i.Ticket)
                .Where(i => i.Ticket.CustomerName == customer.FullName)
                .Select(i => new InvoiceDto
                {
                    Id = i.Id,
                    TicketId = i.TicketId,
                    TransactionId = i.TransactionId,
                    Status = i.Status, 
                    PaymentDate = i.PaymentDate,
                }).ToListAsync();

            if (invoices.Count == 0)
            {
                _logger.LogWarning("No invoice found ");
                throw new UserFriendlyException("invoice not found!");
            }

            return invoices;
        }

        public async Task<List<InvoiceDto>> GetAllInvoicesAsync()
        {
            var invoices = await _dbContext.Invoices
                .Include(i => i.Ticket)
                .Select(i => new InvoiceDto
                {
                    Id = i.Id,
                    TicketId = i.TicketId,
                    TransactionId = i.TransactionId,
                    Status = i.Status,
                    PaymentDate = i.PaymentDate,
                }).ToListAsync();

            if (invoices.Count == 0)
            {
                _logger.LogWarning("No invoice found ");
                throw new UserFriendlyException("invoice not found!");
            }

            return invoices;
        }
    }
}
