using DA.Auth.ApplicationService.Common;
using DA.Auth.ApplicationService.UserModule.Abstracts;
using DA.Auth.Domain;
using DA.Auth.Dtos.CustomerModule;
using DA.Auth.Infrastructure;
using DA.Shared.Dtos;
using DA.Shared.Exceptions;
using DA.Shared.Untils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Auth.ApplicationService.UserModule.Implements
{
    public class AdminService : AuthServiceBase, IAdminService
    {
        private readonly ILogger _logger;
        private readonly AuthDbContext _dbContext;
        public AdminService(ILogger<AdminService> logger, AuthDbContext dbContext) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public PageResultDto<List<CustomerDto>> GetAll(FilterDto input)
        {
            var customerQuery = _dbContext.Customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                FullName = c.FullName,
                PhoneNumber = c.PhoneNumber,
            });
            if (!string.IsNullOrEmpty(input.Keyword))
            {
                customerQuery = customerQuery.Where(c => c.FullName.ToLower().Contains(input.Keyword));
                //or s.Name?.Contains(input.Keyword) ?? false
            }
            int totalItem = customerQuery.Count();
            customerQuery = customerQuery.Skip(input.PageSize * (input.PageIndex - 1))
                .Take(input.PageSize);

            return new PageResultDto<List<CustomerDto>>
            {
                Items = customerQuery.ToList(),
                TotalItem = totalItem,
            };
        }

        public CustomerDto GetCustomer(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault((c) => c.UserId == id);
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

        public void UpdateCustomer(UpdateCustomerByAdminDto input)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.UserId == input.Id);

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

        public void DeleteCustomer(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.UserId == id);

            if (customer == null)
            {
                throw new UserFriendlyException("Customer not found!");
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
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
