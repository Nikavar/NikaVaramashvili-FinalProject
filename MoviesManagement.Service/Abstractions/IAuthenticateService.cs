using MovieWebApi.Domain.POCO;
using MovieWebApi.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieWebApi.Service.Abstractions
{
    public interface IAuthenticateService
    {
        Users Login(string userName, string password);
        Users Registration(UserRegisterServiceModel userModel);
    }
}
