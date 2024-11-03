using DA.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DA.Shared.Untils
{
    public static class CommonUntils
    {
        public static string CreateMD5(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes);
        }

        public static int GetCurrentUserId(IHttpContextAccessor httpContextAccessor)
        {
            var claims = httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
            var claim = claims?.FindFirst(ClaimTypes.Name) ?? claims?.FindFirst("userId");
            if (claim == null)
            {
                throw new UserFriendlyException($"The account does not contain any claims \"{System.Security.Claims.ClaimTypes.NameIdentifier}\"");
            }

            int userId = int.Parse(claim.Value);
            return userId;
        }
    }
}
