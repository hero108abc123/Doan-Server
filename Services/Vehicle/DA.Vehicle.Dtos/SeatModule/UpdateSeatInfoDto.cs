using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.Dtos.SeatModule
{
    internal class UpdateSeatInfoDto
    {
        public int Id { get; set; }
        private string _position;

        [Required]
        [StringLength(30, ErrorMessage = "Position phải có độ dài từ 3 đến 30 ký tự.", MinimumLength = 3)]
        public string Position
        {
            get => _position;
            set => _position = value?.Trim();
        }
    }
}
