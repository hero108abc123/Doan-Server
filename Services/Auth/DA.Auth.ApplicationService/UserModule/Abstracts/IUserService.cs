using DA.Auth.Dtos.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Auth.ApplicationService.UserModule.Abstracts
{
    public interface IUserService
    {
        void CreateUser(CreateUserDto input);
        string Login(LoginDto input);
    }
}
