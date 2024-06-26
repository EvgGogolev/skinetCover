using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiException : ApiResponce
    {
        public ApiException(int stasusCode, string message = null, string details = null) : base(stasusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }

    }
}