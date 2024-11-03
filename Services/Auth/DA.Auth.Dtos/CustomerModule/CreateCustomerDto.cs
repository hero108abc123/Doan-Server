using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Auth.Dtos.CustomerModule
{
    public class CreateCustomerDto
    {
        private string _fullName;

        [Required]
        [StringLength(30, ErrorMessage = "Fullname must be between 3 and 30 characters long.", MinimumLength = 3)]
        public string FullName
        {
            get => _fullName;
            set => _fullName = value?.Trim();
        }

        [Required]
        [RegularExpression(@"^(0|\+84)(3[2-9]|5[6|8|9]|7[0|6|7|8|9]|8[1-5]|9[0-9])[0-9]{7}$", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }
    }
}
