using DA.Payment.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DA.Payment.ApplicationService
{
    public class VNPayService
    {
        public string CreatePaymentUrl(decimal amount, string orderInfo, string returnUrl)
        {
            var vnp_Url = VNPayConfig.Vnp_Url;
            var vnp_TmnCode = VNPayConfig.Vnp_TmnCode;
            var vnp_HashSecret = VNPayConfig.Vnp_HashSecret;

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
                {"vnp_ReturnUrl", returnUrl},
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
    }
}
