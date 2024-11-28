using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.Dtos.SeatModule
{
    public class UpdateSeatIPositionDto
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

        private string _row;

        [Required]
        [StringLength(30, ErrorMessage = "Row phải có độ dài từ 3 đến 30 ký tự.", MinimumLength = 3)]
        public string Row
        {
            get => _row;
            set => _row = value?.Trim();
        }

        private string _floor;

        [Required]
        [StringLength(30, ErrorMessage = "Floor phải có độ dài từ 3 đến 30 ký tự.", MinimumLength = 3)]
        public string Floor
        {
            get => _floor;
            set => _floor = value?.Trim();
        }
    }
}
