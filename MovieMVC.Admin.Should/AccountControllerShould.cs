using Microsoft.AspNetCore.Identity;
using MovieMVC.Admin.Controllers;
using MovieMVC.Admin.Models;
using MovieMVC.Admin.Models.Account;
using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MovieMVC.Admin.Tests
{
    public class AccountControllerShould
    {
        [Fact]
        public void BeUserRoleChecked()
        {
            /// AAA Arange, Act, Assert
            /// 
            ManageAccountRolesViewModel model = new ManageAccountRolesViewModel(); //Arrange
            model.RoleName = "Admin";
            Assert.True(model.RoleName.StartsWith('A')); // Assert(Act)
        }

        [Fact]
        public void CheckedUserNameGreaterFiveChar()
        {
            /// AAA Arange, Act, Assert
            /// 
            ManageAccountRolesViewModel model = new ManageAccountRolesViewModel(); //Arrange
            model.RoleName = "Administrator";
            Assert.True(model.RoleName.Length > 5); // Assert(Act)
        }
    }
}
