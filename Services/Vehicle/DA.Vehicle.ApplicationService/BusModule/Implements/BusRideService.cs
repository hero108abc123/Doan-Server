
using DA.Shared.ApplicationService.Vehicle;
using DA.Shared.Exceptions;
using DA.Vehicle.ApplicationService.Common;
using DA.Vehicle.Domain;
using DA.Vehicle.Dtos.BusRideModule;
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
    public class BusRideService : VehicleServiceBase, IBusRideService
    {
        private readonly VehicleDbContext _dbContext;
        private readonly ILogger _logger;

        public BusRideService(ILogger<BusRideService> logger, VehicleDbContext dbContext) : base(logger, dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<BusRideDto> GetBusRideByIdAsync(int id)
        {
            var ride = await _dbContext.BusRides
                .Include(r => r.VehicleBus) // Include the associated bus
                .FirstOrDefaultAsync(r => r.Id == id);

            if (ride == null)
            {
                throw new UserFriendlyException("Ride not found!");
            }

            return new BusRideDto
            {
                Id = ride.Id,
                RideName = ride.RideName,
                LicensePlate = ride.LicensePlate,
                StartTime = ride.StartTime,
                EndTime = ride.EndTime,
                StartLocation = ride.StartLocation,
                EndLocation = ride.EndLocation,
                BusId = ride.BusId,
                BusType = ride.VehicleBus?.TypeName
            };
        }

        public async Task CreateBusRideAsync(CreateBusRideDto input)
        {
            var bus = await _dbContext.Buses.FirstOrDefaultAsync(b => b.Id == input.BusId);
            if (bus == null)
            {
                throw new UserFriendlyException("Associated bus not found!");
            }

            var newRide = new VehicleBusRide
            {
                RideName = input.RideName,
                LicensePlate = input.LicensePlate,
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                StartLocation = input.StartLocation,
                EndLocation = input.EndLocation,
                BusId = input.BusId
            };

            await _dbContext.BusRides.AddAsync(newRide);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBusRideAsync(UpdateBusRideDto input)
        {
            var ride = await _dbContext.BusRides.FirstOrDefaultAsync(r => r.Id == input.Id);

            if (ride == null)
            {
                throw new UserFriendlyException("Ride not found!");
            }

            var bus = await _dbContext.Buses.FirstOrDefaultAsync(b => b.Id == input.BusId);
            if (bus == null)
            {
                throw new UserFriendlyException("Associated bus not found!");
            }

            ride.RideName = input.RideName;
            ride.LicensePlate = input.LicensePlate;
            ride.StartTime = input.StartTime;
            ride.EndTime = input.EndTime;
            ride.StartLocation = input.StartLocation;
            ride.EndLocation = input.EndLocation;
            ride.BusId = input.BusId;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBusRideAsync(int id)
        {
            var ride = await _dbContext.BusRides.FirstOrDefaultAsync(r => r.Id == id);

            if (ride == null)
            {
                throw new UserFriendlyException("Ride not found!");
            }

            _dbContext.BusRides.Remove(ride);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<SeatDto>> GetAllSeatsAsync(int busRideId)
        {
            _logger.LogInformation("Fetching all seats for BusRideId: {BusRideId}", busRideId);

            var seats = await _dbContext.Seats
                .Include(s => s.BusRide) // Bao gồm thông tin của Bus
                .Where(s => s.BusId == busRideId)
                .Select(s => new SeatDto
                {
                    Id = s.Id,
                    Position = s.Position,
                    Row = s.Row,
                    Floor = s.Floor,
                    Status = s.Status,
                })
                .ToListAsync();

            if (!seats.Any())
            {
                _logger.LogWarning("No seats found for BusRideId: {BusRideId}", busRideId);
            }

            return seats;
        }

        public async Task<List<SeatDto>> GetAvailableSeatsAsync(int busRideId)
        {
            _logger.LogInformation("Fetching available seats (Status = 0) for BusRideId: {BusRideId}", busRideId);

            var seats = await _dbContext.Seats
                .Include(s => s.BusRide) // Bao gồm thông tin của Bus
                .Where(s => s.Status == 0)
                .Select(s => new SeatDto
                {
                    Id = s.Id,
                    Position = s.Position,
                    Row = s.Row,
                    Floor = s.Floor,
                    Status = s.Status,
                })
                .ToListAsync();

            if (!seats.Any())
            {
                _logger.LogWarning("No available seats found for BusRideId: {BusRideId}", busRideId);
            }

            return seats;
        }

        public async Task<SeatWithBusRideDto> GetAvailableSeatWithBusRideInfoAsync(int busRideId, int seatId)
        {
            _logger.LogInformation("Fetching details for SeatId: {SeatId} in BusRideId: {BusRideId}", seatId, busRideId);

            // Lấy thông tin ghế và kiểm tra xem ghế có thuộc về chuyến xe chỉ định không
            var seat = await _dbContext.Seats
                .Include(s => s.BusRide) // Bao gồm thông tin của Bus// Bao gồm thông tin của các BusRide liên quan
                .FirstOrDefaultAsync(s => s.Id == seatId);

            if (seat == null)
            {
                _logger.LogWarning("Seat not found or does not belong to BusRideId: {BusRideId}", busRideId);
                throw new UserFriendlyException("Seat not found or does not belong to the specified bus ride!");
            }

            // Kiểm tra ghế có trống hay không (Status = 0 nghĩa là trống)
            if (seat.Status != 0)
            {
                _logger.LogWarning("Seat with SeatId: {SeatId} in BusRideId: {BusRideId} is not available. Current Status: {Status}", seatId, busRideId, seat.Status);
                throw new UserFriendlyException("The seat is not available!");
            }

            // Lấy thông tin chuyến xe
            var busRide = await _dbContext.BusRides.Include(br => br.VehicleBus).FirstOrDefaultAsync(br => br.Id == seat.BusId);

            if (busRide == null)
            {
                _logger.LogWarning("Bus ride not found for SeatId: {SeatId} and BusRideId: {BusRideId}", seatId, busRideId);
                throw new UserFriendlyException("Bus ride not found for this seat!");
            }

            return new SeatWithBusRideDto
            {
                SeatId = seat.Id,
                Position = seat.Position,
                Row = seat.Row,
                Floor = seat.Floor,
                Status = seat.Status,
                BusId = busRide.BusId,
                Price = busRide.VehicleBus.Price,
                BusRideId = busRide.Id,
                BusRideName = busRide.RideName,
                StartTime = busRide.StartTime,
                EndTime = busRide.EndTime,
                StartLocation = busRide.StartLocation,
                EndLocation = busRide.EndLocation
            };
        }

        public async Task UpdateSeatStatusBybusRide(int busRideId, int seatId, int status)
        {
            var seat = await _dbContext.Seats
             .Include(s => s.BusRide)
             .FirstOrDefaultAsync(s => s.Id == seatId);

            if (seat == null)
            {
                _logger.LogWarning("Seat not found or does not belong to BusRideId: {BusRideId}", busRideId);
                throw new UserFriendlyException("Seat not found or does not belong to the specified bus ride!");
            }

            // Kiểm tra ghế có trống hay không (Status = 0 nghĩa là trống)
            if (seat.Status != 0)
            {
                _logger.LogWarning("Seat with SeatId: {SeatId} in BusRideId: {BusRideId} is not available. Current Status: {Status}", seatId, busRideId, seat.Status);
                throw new UserFriendlyException("The seat is not available!");
            }

            seat.Status = status;
            await _dbContext.SaveChangesAsync();
        }
    }
}
