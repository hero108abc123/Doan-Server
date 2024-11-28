using DA.Booking.Infrastucture;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Booking.ApplicationService.Common
{
    public abstract class BookingServiceBase
    {
        protected readonly ILogger _logger;
        protected readonly BookingDbContext _dbContext;

        protected BookingServiceBase(ILogger logger, BookingDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
    }
}
