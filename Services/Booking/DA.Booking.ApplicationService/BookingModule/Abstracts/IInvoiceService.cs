using DA.Booking.Dtos.InvoiceModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Booking.ApplicationService.BookingModule.Abstracts
{
    public interface IInvoiceService
    {
        Task <List<InvoiceDto>> GetInvoicesByCustomerAsync();
        Task<List<InvoiceDto>> GetAllInvoicesAsync();
    }
}
