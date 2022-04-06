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


        public string message { get; set; }
        public UserBL user { get; set; }
    }


    public class TurnstileBLException : Exception
    {

        public TurnstileBLException(string exception_message)
        {
            this.message = exception_message;
        }
        public TurnstileBLException(string exception_message, TurnstileBL turnstile)
        {
            this.message = exception_message;
            this.turnstile = turnstile;
        }


        public string message { get; set; }
        public TurnstileBL turnstile { get; set; }
    }

    public class LiftBLException : Exception
    {

        public LiftBLException(string exception_message)
        {
            this.message = exception_message;
        }
        public LiftBLException(string exception_message, LiftBL obj)
        {
            this.message = exception_message;
            this.lift = obj;
        }


        public string message { get; set; }
        public LiftBL lift { get; set; }
    }

    public class SlopeBLException : Exception
    {

        public SlopeBLException(string exception_message)
        {
            this.message = exception_message;
        }
        public SlopeBLException(string exception_message, SlopeBL obj)
        {
            this.message = exception_message;
            this.slope = obj;
        }


        public string message { get; set; }
        public SlopeBL slope { get; set; }
    }

    public class LiftSlopeBLException : Exception
    {

        public LiftSlopeBLException(string exception_message)
        {
            this.message = exception_message;
        }
        public LiftSlopeBLException(string exception_message, LiftSlopeBL obj)
        {
            this.message = exception_message;
            this.lift_slope = obj;
        }


        public string message { get; set; }
        public LiftSlopeBL lift_slope { get; set; }
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
