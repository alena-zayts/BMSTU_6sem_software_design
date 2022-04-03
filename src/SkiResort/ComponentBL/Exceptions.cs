using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentAccessToDB
{
    public class UserBLException : Exception
    {
        
        public UserBLException(string exception_message)
        {
            this.exception_message = exception_message;
        }
        public string exception_message { get; }
    }
}
