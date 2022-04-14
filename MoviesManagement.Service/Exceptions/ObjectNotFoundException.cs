using System;
using System.Collections.Generic;
using System.Text;

namespace MovieWebApi.Service.Exceptions
{
    [Serializable]
    public class ObjectNotFoundException : Exception
    {
        public string StatusCode = "code: 404, ObjectNotFound";

        public ObjectNotFoundException(string errorMsg) : base(errorMsg) { }
    }
}
