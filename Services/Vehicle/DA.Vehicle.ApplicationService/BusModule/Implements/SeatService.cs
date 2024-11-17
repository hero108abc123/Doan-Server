using DA.Shared.Exceptions;
using DA.Vehicle.ApplicationService.BusModule.Abstracts;
using DA.Vehicle.ApplicationService.Common;
using DA.Vehicle.Domain;
using DA.Vehicle.Dtos.SeatModule;
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
    public class SeatService : VehicleServiceBase, ISeatService
    {
        private readonly ILogger _logger;
        private readonly VehicleDbContext _dbContext;
        public SeatService(ILogger<SeatService> logger, VehicleDbContext dbContext) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void AddSeat(CreateSeatDto input)
        {
            var bus = _dbContext.Buses.FirstOrDefault(b => b.Id == input.BusId);
            if (bus == null)
            {
                throw new UserFriendlyException("Bus not found!");
            }

            var seat = new VehicleSeat
            {
                BusId = input.BusId,
                Position = input.Position,
                IsAvailable = 0,
            };

            _dbContext.Seats.Add(seat);
            _dbContext.SaveChanges();
        }

        public void UpdateSeatPosition(UpdateSeatIPositionDto input)
        {
            var seat = _dbContext.Seats.FirstOrDefault(s => s.Id == input.Id);
            if (seat == null)
            {
                throw new UserFriendlyException("Seat not found!");
            }

            seat.Position = input.Position;
            _dbContext.SaveChanges();
        }

        public void DeleteSeat(int seatId)
        {
            var seat = _dbContext.Seats.FirstOrDefault(s => s.Id == seatId);
            if (seat == null)
            {
                throw new UserFriendlyException("Seat not found!");
            }

            _dbContext.Seats.Remove(seat);
            _dbContext.SaveChanges();
        }

    }
}
