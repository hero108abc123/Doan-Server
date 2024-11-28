using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Booking.ApplicationService.Config
{
    public class VnPayConfig
    {
        public const string Vnp_TmnCode = "YOUR_TMNCODE";
        public const string Vnp_HashSecret = "YOUR_HASH_SECRET";
        public const string Vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public const string Vnp_ReturnUrl = "https://your-return-url";
    }
}
