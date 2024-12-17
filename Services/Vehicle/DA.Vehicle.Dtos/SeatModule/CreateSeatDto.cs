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
        public string Position
        {
            get => _position;
            set => _position = value?.Trim();
        }

        private string _row;

        [Required]
        public string Row
        {
            get => _row;
            set => _row = value?.Trim();
        }

        private string _floor;

        [Required]
        public string Floor
        {
            get => _floor;
            set => _floor = value?.Trim();
        }

        public int BusId { get; set; }
    }
}
