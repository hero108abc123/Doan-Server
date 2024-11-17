using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.Dtos.SeatModule
{
    public class CreateSeatDto
    {
        private string _position;

        [Required]
        [StringLength(30, ErrorMessage = "Position phải có độ dài từ 3 đến 30 ký tự.", MinimumLength = 3)]
        public string Position
        {
            get => _position;
            set => _position = value?.Trim();
        }

        public int BusId { get; set; }
    }
}
