using DA.Vehicle.Dtos.SeatModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.ApplicationService.BusModule.Abstracts
{
    public interface ISeatService
    {
        Task AddSeatAsync(CreateSeatDto input);
        Task UpdateSeatPositionAsync(UpdateSeatIPositionDto input);
        Task UpdateSeatStatusAsync(int id, int status);
        Task DeleteSeatAsync(int seatId);
    }
}
