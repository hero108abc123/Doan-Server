using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.Dtos.BusModule
{
    public class CreateBusDto
    {
        private string _typeName;

        [Required]
        [StringLength(30, ErrorMessage = "Typename phải có độ dài từ 3 đến 30 ký tự.", MinimumLength = 3)]
        public string TypeName
        {
            get => _typeName;
            set => _typeName = value?.Trim();
        }
        public decimal Price { get; set; }
        public int TotalSeat { get; set; }
    }
}
