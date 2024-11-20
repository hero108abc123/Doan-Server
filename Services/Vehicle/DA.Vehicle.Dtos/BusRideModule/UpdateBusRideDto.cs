using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.Dtos.BusRideModule
{
    public class UpdateBusRideDto
    {
        public int Id { get; set; }

        private string _rideName;

        [Required]
        [StringLength(30, ErrorMessage = "RideName phải có độ dài từ 3 đến 30 ký tự.", MinimumLength = 3)]
        public string RideName
        {
            get => _rideName;
            set => _rideName = value?.Trim();
        }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        private string _startLocation;

        [Required]
        [StringLength(30, ErrorMessage = "StartLocation phải có độ dài từ 3 đến 30 ký tự.", MinimumLength = 3)]
        public string StartLocation
        {
            get => _startLocation;
            set => _startLocation = value?.Trim();
        }
        private string _endLocation;

        [Required]
        [StringLength(30, ErrorMessage = "EndLocation phải có độ dài từ 3 đến 30 ký tự.", MinimumLength = 3)]
        public string EndLocation
        {
            get => _endLocation;
            set => _endLocation = value?.Trim();
        }
        public int BusId { get; set; }
    }
}
