using DA.Shared.Exceptions;
using DA.Shared.Untils;
using DA.Vehicle.ApplicationService.BusModule.Abstracts;
using DA.Vehicle.ApplicationService.Common;
using DA.Vehicle.Domain;
using DA.Vehicle.Dtos.BusModule;
using DA.Vehicle.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.ApplicationService.BusModule.Implements
{
    public class BusService : VehicleServiceBase, IBusService
    {
        private readonly ILogger _logger;
        private readonly VehicleDbContext _dbContext;
        public BusService(ILogger<BusService> logger, VehicleDbContext dbContext) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void AddBus(CreateBusDto input) 
        {
            bool nameExists = _dbContext.Buses.Any(b => b.TypeName == input.TypeName);
            if (nameExists)
            {
                throw new UserFriendlyException("This name is already in use by another bus type.");
            }
            var newBus = new VehicleBus
            {
                TypeName = input.TypeName,
                Price = input.Price,
                TotalSeat = input.TotalSeat,
            };
            _dbContext.Buses.Add(newBus);
            _dbContext.SaveChanges();
        }

        public void UpdateBus(UpdateBusDto input)
        {

            var bus = _dbContext.Buses.FirstOrDefault(b => b.Id == input.Id);

            if (bus == null)
            {
                throw new UserFriendlyException("Bus not found!");
            }

            bool nameExists = _dbContext.Buses
                .Any(b => b.TypeName == input.TypeName && b.Id != bus.Id);

            if (nameExists)
            {
                throw new UserFriendlyException("This name is already exit.");
            }

            bus.TypeName = input.TypeName;
            bus.Price = input.Price;
            bus.TotalSeat = input.TotalSeat;
            _dbContext.SaveChanges();
        }

        public void DeleteBus(int id)
        {
            var bus = _dbContext.Buses.FirstOrDefault(b => b.Id == id);

            if (bus == null)
            {
                throw new UserFriendlyException("Bus not found!");
            }

            _dbContext.Buses.Remove(bus);
            _dbContext.SaveChanges();
        }
    }
}
