using System;
using System.Collections.Generic;
using System.Text;

namespace MovieWebApi.Service.Models.JWT
{
    public class JWTConfig
    {
        public string Secret { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
