using DA.Vehicle.Dtos.BusRideModule;
using DA.Vehicle.Dtos.SeatModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Shared.ApplicationService.Vehicle
{
    public interface IBusRideService
    {
        Task<BusRideDto> GetBusRideByIdAsync(int id);
        Task CreateBusRideAsync(CreateBusRideDto input);
        Task UpdateBusRideAsync(UpdateBusRideDto input);
        Task DeleteBusRideAsync(int id);
        Task<List<SeatDto>> GetAllSeatsAsync(int busRideId);
        Task<List<SeatDto>> GetAvailableSeatsAsync(int busRideId);
        Task<SeatWithBusRideDto> GetAvailableSeatWithBusRideInfoAsync(int busRideId,int seatId);

        Task UpdateSeatStatusBybusRide(int busRideId, int SeatId, int status);
    }
}
