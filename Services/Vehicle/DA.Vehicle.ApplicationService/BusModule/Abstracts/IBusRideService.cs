using DA.Vehicle.Dtos.BusRideModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.ApplicationService.BusModule.Abstracts
{
    public interface IBusRideService
    {
        BusRideDto GetBusRideById(int id);
        void CreateBusRide(CreateBusRideDto input);
        void UpdateBusRide(UpdateBusRideDto input);
        void DeleteBusRide(int id);
    }
}
