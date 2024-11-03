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
        [StringLength(30, ErrorMessage = "Email must be between 3 and 30 characters long.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email.")]
        public string Email
        {
            get => _email;
            set => _email = value?.Trim();
        }

        private string _password;

        [Required]
        [StringLength(30, ErrorMessage = "Password must be between 3 and 30 characters long.", MinimumLength = 3)]
        public string Password
        {
            get => _password;
            set => _password = value?.Trim();
        }

        public int UserType { get; set; } = 2;
    }
}
