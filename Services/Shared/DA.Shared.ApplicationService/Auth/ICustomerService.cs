using DA.Auth.Dtos.CustomerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Shared.ApplicationService.Auth
{
    public interface ICustomerService
    {
        CustomerDto GetCustomer();
        void CreateCustomer(CreateCustomerDto input);
        void UpdateCustomer(UpdateCustomerDto input);
        void DeleteCustomer();
    }
}
