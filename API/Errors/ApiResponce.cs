using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLitePCL;

namespace API.Errors
{
    public class ApiResponce
    {
        public ApiResponce(int stasusCode, string message = null) 
        {
            StatusCode = stasusCode;
            Message = message ?? GetDefaultMessageForStatusCode(stasusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageForStatusCode(int stasusCode)
        {
            return stasusCode switch 
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change",
                _ => null
            };
        }
    }
}