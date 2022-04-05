using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ComponentBL.ModelsBL;

namespace ComponentBL
{
    public class UserBLException : Exception
    {
        
        public UserBLException(string exception_message)
        {
            this.message = exception_message;
        }
        public UserBLException(string exception_message, UserBL user)
        {
            this.message = exception_message;
            this.user = user;
        }


        public string message { get; }
        public UserBL user;
    }
}
