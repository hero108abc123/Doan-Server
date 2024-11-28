using DA.Vehicle.Dtos.BusRideModule;
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
        Task<SeatWithBusRideDto> GetSeatWithBusRide(int BusRideId,int SeatId);
    }
}
