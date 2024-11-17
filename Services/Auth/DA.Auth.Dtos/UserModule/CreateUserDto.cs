using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Auth.Dtos.UserModule
{
    public class CreateUserDto
    {
        private string _email;

        [Required]
        [StringLength(30, ErrorMessage = "Email phải có độ dài từ 3 đến 30 ký tự.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$", ErrorMessage = "Sai định dạng Email.")]
        public string Email
        {
            get => _email;
            set => _email = value?.Trim();
        }

        private string _password;

        [Required]
        [StringLength(30, ErrorMessage = "Password phải có độ dài từ 3 đến 30 ký tự.", MinimumLength = 3)]
        public string Password
        {
            get => _password;
            set => _password = value?.Trim();
        }

        public int UserType { get; set; } = 2;
    }
}
