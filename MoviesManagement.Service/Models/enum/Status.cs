using System;
using System.Collections.Generic;
using System.Text;

namespace MovieWebApi.Service
{
    public enum Status
    {
        NotFound = 404,
        AlreadyExistrs = 409,
        InternalServerError = 500,
        Success = 200
    }
}
