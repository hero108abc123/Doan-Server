using DA.Auth.ApplicationService.Common;
using DA.Auth.ApplicationService.UserModule.Abstracts;
using DA.Auth.Domain;
using DA.Auth.Dtos.CustomerModule;
using DA.Auth.Infrastructure;
using DA.Shared.Exceptions;
using DA.Shared.Untils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Auth.ApplicationService.UserModule.Implements
{
    public class CustomerService : AuthServiceBase, ICustomerService
    {
        private readonly ILogger _logger;
        private readonly AuthDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomerService(ILogger<CustomerService> logger, AuthDbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public CustomerDto GetCustomer()
        {
            int currentUserId = CommonUntils.GetCurrentUserId(_httpContextAccessor);
            var customer = _dbContext.Customers.FirstOrDefault((c) => c.UserId == currentUserId);
            if (customer != null)
            {
                var result = new CustomerDto
                {
                    Id = customer.Id,
                    FullName = customer.FullName,
                    PhoneNumber = customer.PhoneNumber,
                };
                return result;
            }
            throw new UserFriendlyException($"User not found!");

        }

        public void CreateCustomer(CreateCustomerDto input)
        {
            int currentUserId = CommonUntils.GetCurrentUserId(_httpContextAccessor);
            bool nameExists = _dbContext.Customers.Any(c => c.FullName == input.FullName);
            if (nameExists)
            {
                throw new UserFriendlyException("This name is already in use by another customer.");
            }

            // Check if a customer already exists for the current user ID
            bool customerExistsForUser = _dbContext.Customers.Any(c => c.UserId == currentUserId);
            if (customerExistsForUser)
            {
                throw new UserFriendlyException("A customer profile already exists for this user.");
            }
            var newCustomer = new AuthCustomer
            {
                FullName = input.FullName,
                PhoneNumber = input.PhoneNumber,
                UserId = currentUserId
            };
            _dbContext.Customers.Add(newCustomer);
            _dbContext.SaveChanges();
        }


        public void UpdateCustomer(UpdateCustomerDto input)
        {
            int currentUserId = CommonUntils.GetCurrentUserId(_httpContextAccessor);

            var customer = _dbContext.Customers.FirstOrDefault(c => c.UserId == currentUserId);

            if (customer == null)
            {
                throw new UserFriendlyException("Customer not found!");
            }

            bool nameExists = _dbContext.Customers
                .Any(c => c.FullName == input.FullName && c.Id != customer.Id);

            if (nameExists)
            {
                throw new UserFriendlyException("This name is already in use by another customer.");
            }

            customer.FullName = input.FullName;
            customer.PhoneNumber = input.PhoneNumber;
            _dbContext.SaveChanges();
        }

        public void DeleteCustomer()
        {
            int currentUserId = CommonUntils.GetCurrentUserId(_httpContextAccessor);
            var customer = _dbContext.Customers.FirstOrDefault(c => c.UserId == currentUserId);

            if (customer == null)
            {
                throw new UserFriendlyException("Customer not found!");
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == currentUserId);
            if (user == null)
            {
                throw new UserFriendlyException("User not found!");
            }


            _dbContext.Customers.Remove(customer);
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

    }
}
