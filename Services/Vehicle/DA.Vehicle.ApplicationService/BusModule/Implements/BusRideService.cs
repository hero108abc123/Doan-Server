using DA.Shared.Exceptions;
using DA.Vehicle.ApplicationService.BusModule.Abstracts;
using DA.Vehicle.ApplicationService.Common;
using DA.Vehicle.Domain;
using DA.Vehicle.Dtos.BusRideModule;
using DA.Vehicle.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.ApplicationService.BusModule.Implements
{
    public class BusRideService : VehicleServiceBase, IBusRideService
    {
        private readonly VehicleDbContext _dbContext;
        private readonly ILogger _logger;

        public BusRideService(ILogger<BusRideService> logger, VehicleDbContext dbContext) : base(logger, dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;   
        }

        public BusRideDto GetBusRideById(int id)
        {
            var ride = _dbContext.BusRides
                .Include(r => r.VehicleBus) // Include the associated bus
                .FirstOrDefault(r => r.Id == id);

            if (ride == null)
            {
                throw new UserFriendlyException("Ride not found!");
            }

            return new BusRideDto
            {
                Id = ride.Id,
                RideName = ride.RideName,
                StartTime = ride.StartTime,
                EndTime = ride.EndTime,
                StartLocation = ride.StartLocation,
                EndLocation = ride.EndLocation,
                BusId = ride.BusId,
                BusType = ride.VehicleBus?.TypeName
            };
        }

        public void CreateBusRide(CreateBusRideDto input)
        {
            var bus = _dbContext.Buses.FirstOrDefault(b => b.Id == input.BusId);
            if (bus == null)
            {
                throw new UserFriendlyException("Associated bus not found!");
            }

            var newRide = new VehicleBusRide
            {
                RideName = input.RideName,
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                StartLocation = input.StartLocation,
                EndLocation = input.EndLocation,
                BusId = input.BusId
            };

            _dbContext.BusRides.Add(newRide);
            _dbContext.SaveChanges();
        }

        public void UpdateBusRide(UpdateBusRideDto input)
        {
            var ride = _dbContext.BusRides.FirstOrDefault(r => r.Id == input.Id);

            if (ride == null)
            {
                throw new UserFriendlyException("Ride not found!");
            }

            var bus = _dbContext.Buses.FirstOrDefault(b => b.Id == input.BusId);
            if (bus == null)
            {
                throw new UserFriendlyException("Associated bus not found!");
            }

            ride.RideName = input.RideName;
            ride.StartTime = input.StartTime;
            ride.EndTime = input.EndTime;
            ride.StartLocation = input.StartLocation;
            ride.EndLocation = input.EndLocation;
            ride.BusId = input.BusId;

            _dbContext.SaveChanges();
        }

        public void DeleteBusRide(int id)
        {
            var ride = _dbContext.BusRides.FirstOrDefault(r => r.Id == id);

            if (ride == null)
            {
                throw new UserFriendlyException("Ride not found!");
            }

            _dbContext.BusRides.Remove(ride);
            _dbContext.SaveChanges();
        }

    }
}
