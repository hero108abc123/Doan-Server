using DA.Shared.Exceptions;
using DA.Shared.Untils;
using DA.Vehicle.ApplicationService.BusModule.Abstracts;
using DA.Vehicle.ApplicationService.Common;
using DA.Vehicle.Domain;
using DA.Vehicle.Dtos.BusModule;
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
    public class BusService : VehicleServiceBase, IBusService
    {
        private readonly ILogger _logger;
        private readonly VehicleDbContext _dbContext;
        public BusService(ILogger<BusService> logger, VehicleDbContext dbContext) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task AddBusAsync(CreateBusDto input)
        {
                bool nameExists = await _dbContext.Buses.AnyAsync(b => b.TypeName == input.TypeName);
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

                await _dbContext.Buses.AddAsync(newBus);
                await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBusAsync(UpdateBusDto input)
        {
                var bus = await _dbContext.Buses.FirstOrDefaultAsync(b => b.Id == input.Id);

                if (bus == null)
                {
                    throw new UserFriendlyException("Bus not found!");
                }

                bool nameExists = await _dbContext.Buses
                    .AnyAsync(b => b.TypeName == input.TypeName && b.Id != bus.Id);

                if (nameExists)
                {
                    throw new UserFriendlyException("This name is already in use.");
                }

                bus.TypeName = input.TypeName;
                bus.Price = input.Price;
                bus.TotalSeat = input.TotalSeat;
                await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBusAsync(int id)
        {
                var bus = await _dbContext.Buses.FirstOrDefaultAsync(b => b.Id == id);

                if (bus == null)
                {
                    throw new UserFriendlyException("Bus not found!");
                }

                _dbContext.Buses.Remove(bus);
                await _dbContext.SaveChangesAsync();
        }

        public async Task<List<SeatDto>> GetAllSeatsAsync(int busId)
        {
                _logger.LogInformation("Fetching all seats for BusId: {BusId}", busId);

                var seats = await _dbContext.Seats
                    .Include(s => s.VehicleBus)
                    .Where(s => s.BusId == busId)
                    .Select(s => new SeatDto
                    {
                        Id = s.Id,
                        Position = s.Position,
                        Row = s.Row,
                        Floor = s.Floor,
                        Status = s.Status,
                    })
                    .ToListAsync();

                if (seats.Count == 0)
                {
                    _logger.LogWarning("No seats found for BusId: {BusId}", busId);
                    throw new UserFriendlyException("Seat not found!");
                }

                return seats;
        }

        public async Task<List<SeatDto>> GetSeatsByStatusAsync(SeatWithStatusDto input)
        {
                _logger.LogInformation("Fetching seats with Status: {Status} for BusId: {BusId}", input.Status, input.BusId);

                var seats = await _dbContext.Seats
                    .Include(s => s.VehicleBus)
                    .Where(s => s.BusId == input.BusId && s.Status == input.Status)
                    .Select(s => new SeatDto
                    {
                        Id = s.Id,
                        Position = s.Position,
                        Row = s.Row,
                        Floor = s.Floor,
                        Status = s.Status,
                    })
                    .ToListAsync();

                if (seats.Count == 0)
                {
                    _logger.LogWarning("No seats with Status: {Status} found for BusId: {BusId}", input.Status, input.BusId);
                     throw new UserFriendlyException("Seat not found!");
                }

                return seats;
        }
    }
}
