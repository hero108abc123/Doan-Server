using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Payment.Dtos
{
    public class VNPayRequestDto
    {
        public decimal Amount { get; set; }
        public string OrderInfo { get; set; }
        public string ReturnUrl { get; set; }
    }
}
