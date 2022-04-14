using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC.Admin.Models.Account
{
    public class ManageAccountRolesViewModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}
