using DA.Auth.Dtos.CustomerModule;
using DA.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Auth.ApplicationService.UserModule.Abstracts
{
    public interface IAdminService
    {
        void DeleteCustomer(int id);
        Task <List<CustomerDto>> GetAll();
        CustomerDto GetCustomer(int id);
        void UpdateCustomer(UpdateCustomerByAdminDto input);
    }
}
