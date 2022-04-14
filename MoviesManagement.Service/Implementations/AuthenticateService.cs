using Mapster;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieWebApi.Domain.POCO;
using MovieWebApi.PersistenceDB.Context;
using MovieWebApi.Service.Abstractions;
using MovieWebApi.Service.Models;
using MovieWebApi.Service.Models.JWT;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MovieWebApi.Service.Implementations
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly MovieDbContext _context;
        private readonly AppSettings _appSettings;
        public AuthenticateService(IOptions<AppSettings> appSettings, MovieDbContext context)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public Users Login(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == username && x.Password == password);
            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,"User"),
                    new Claim(ClaimTypes.Version,"V3.1")
                }),
                Expires = DateTime.UtcNow.AddDays(1), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) 
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;
            return user;
        }

        public Users Registration(UserRegisterServiceModel userModel)
        {
            var registerUser = userModel.Adapt<Users>();
            var result = _context.Users.SingleOrDefault(x => x.UserName == registerUser.UserName);

            if (result != null) return null; 

            _context.Users.Add(registerUser);
            _context.SaveChanges();
            return result;
        }

    }
}
