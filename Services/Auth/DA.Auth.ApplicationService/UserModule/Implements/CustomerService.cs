using DA.Auth.ApplicationService.Common;
using DA.Auth.ApplicationService.UserModule.Abstracts;
using DA.Auth.Infrastructure;
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
        public CustomerService(ILogger<UserService> logger, AuthDbContext dbContext) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }


    }
}
