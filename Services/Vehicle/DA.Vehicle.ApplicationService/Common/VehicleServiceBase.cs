using DA.Vehicle.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.ApplicationService.Common
{
    public abstract class VehicleServiceBase
    {
        protected readonly ILogger _logger;
        protected readonly VehicleDbContext _dbContext;

        protected VehicleServiceBase(ILogger logger, VehicleDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
    }
}
