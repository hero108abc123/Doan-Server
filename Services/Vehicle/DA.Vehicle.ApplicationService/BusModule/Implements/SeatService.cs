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

        public async Task AddSeatAsync(CreateSeatDto input)
        {
            var bus = await _dbContext.Buses.FirstOrDefaultAsync(b => b.Id == input.BusId);
            if (bus == null)
            {
                throw new UserFriendlyException("Bus not found!");
            }

            var seat = new VehicleSeat
            {
                BusId = input.BusId,
                Position = input.Position,
                Row = input.Row,
                Floor = input.Floor,
                Status = 0,
            };

            await _dbContext.Seats.AddAsync(seat);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSeatPositionAsync(UpdateSeatIPositionDto input)
        {
            var seat = await _dbContext.Seats.FirstOrDefaultAsync(s => s.Id == input.Id);
            if (seat == null)
            {
                throw new UserFriendlyException("Seat not found!");
            }

            seat.Position = input.Position;
            seat.Row = input.Row;
            seat.Floor = input.Floor;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSeatStatusAsync(UpdateSeatStatusDto input)
        {
            var seat = await _dbContext.Seats.FirstOrDefaultAsync(s => s.Id == input.Id);
            if (seat == null)
            {
                throw new UserFriendlyException("Seat not found!");
            }

            seat.Status = input.Status;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSeatAsync(int seatId)
        {
            var seat = await _dbContext.Seats.FirstOrDefaultAsync(s => s.Id == seatId);
            if (seat == null)
            {
                throw new UserFriendlyException("Seat not found!");
            }

            _dbContext.Seats.Remove(seat);
            await _dbContext.SaveChangesAsync();
        }


    }
}
