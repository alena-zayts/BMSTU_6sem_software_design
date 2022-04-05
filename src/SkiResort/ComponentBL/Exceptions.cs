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

    public class MessageBLException: Exception
    {
        public MessageBLException(string exception_message)
        {
            this.text = exception_message;
        }
        public MessageBLException(string exception_message, MessageBL message)
        {
            this.text = exception_message;
            this.message = message;
        }

        public string text { get; set; }
        public MessageBL message { get; set; }
    }

    public class PermissionsException: Exception
    {
        public PermissionsException(uint user_id, string func_name)
        {
            this.user_id = user_id;
            this.func_name = func_name;
        }

        public uint user_id { get; set; }
        public string func_name { get; set; }
    }
}
