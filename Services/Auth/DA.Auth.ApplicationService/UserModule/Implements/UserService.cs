using DA.Auth.ApplicationService.Common;
using DA.Auth.ApplicationService.UserModule.Abstracts;
using DA.Auth.Domain;
using DA.Auth.Dtos.UserModule;
using DA.Auth.Infrastructure;
using DA.Shared.Constant.Permission;
using DA.Shared.Exceptions;
using DA.Shared.Untils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DA.Auth.ApplicationService.UserModule.Implements
{
    public class UserService : AuthServiceBase, IUserService
    {
        private readonly ILogger _logger;
        private readonly AuthDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public UserService(ILogger<UserService> logger, AuthDbContext dbContext, IConfiguration configuration) : base(logger, dbContext)
        {
            _configuration = configuration;
            _logger = logger;
            _dbContext = dbContext;
        }

        public void CreateUser(CreateUserDto input)
        {
            if (_dbContext.Users.Any(u => u.Email == input.Email))
            {
                throw new UserFriendlyException($"The account name \"{input.Email}\" already exists!");
            }
            var user = _dbContext.Users.Add(new AuthUser
            {
                Email = input.Email,
                Password = PasswordHasher.HashPassword(input.Password),
                UserType = input.UserType,
            });
            _dbContext.SaveChanges();
        }


        public string Login(LoginDto input)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == input.Email);
            if (user == null)
            {
                throw new UserFriendlyException($"User not found!");
            }

            if (PasswordHasher.VerifyPassword(input.Password, user.Password))
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("userId", user.Id.ToString()),
                    new Claim("userEmail", user.Email),
                    new Claim(CustomClaimTypes.UserType, user.UserType.ToString())
                };
            
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddSeconds(_configuration.GetValue<int>("JWT:Expires")),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                throw new UserFriendlyException($"Wrong password!");
            }
        }
    }
}
