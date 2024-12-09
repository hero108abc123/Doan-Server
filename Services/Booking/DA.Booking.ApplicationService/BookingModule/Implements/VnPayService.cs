using DA.Booking.ApplicationService.BookingModule.Abstracts;
using DA.Booking.ApplicationService.Common;
using DA.Booking.ApplicationService.Config;
using DA.Booking.Infrastucture;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DA.Booking.ApplicationService.BookingModule.Implements
{
    public class VnPayService : BookingServiceBase,IPaymentService
    {
        private readonly ILogger _logger;
        private readonly BookingDbContext _dbContext;

        public VnPayService(ILogger<VnPayService> logger, BookingDbContext dbContext) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;

        }

        public string GeneratePaymentLink(decimal amount, string orderInfo)
        {
            var vnp_Url = VnPayConfig.Vnp_Url;
            var vnp_TmnCode = VnPayConfig.Vnp_TmnCode;
            var vnp_HashSecret = VnPayConfig.Vnp_HashSecret;

            var vnp_Params = new SortedDictionary<string, string>
            {
                {"vnp_Version", "2.1.0"},
                {"vnp_Command", "pay"},
                {"vnp_TmnCode", vnp_TmnCode},
                {"vnp_Amount", ((int)(amount * 100)).ToString()},
                {"vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")},
                {"vnp_CurrCode", "VND"},
                {"vnp_IpAddr", "127.0.0.1"},
                {"vnp_Locale", "vn"},
                {"vnp_OrderInfo", orderInfo},
                {"vnp_TxnRef", DateTime.Now.Ticks.ToString()}
            };

            var queryString = new StringBuilder();
            foreach (var param in vnp_Params)
            {
                if (!string.IsNullOrEmpty(param.Value))
                {
                    queryString.AppendFormat("{0}={1}&", param.Key, param.Value);
                }
            }

            var rawData = queryString.ToString().TrimEnd('&');
            var hash = HashSHA256(rawData, vnp_HashSecret);

            return $"{vnp_Url}?{rawData}&vnp_SecureHash={hash}";
        }

        private static string HashSHA256(string input, string key)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            var hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
        }


        public string CheckPaymentStatus(string ticketId)
        {
            // Gọi API VNPay để kiểm tra trạng thái
            // Ví dụ trả về "Paid" hoặc "Pending"
            return "Paid"; // Đây là mock
        }
    }
}
