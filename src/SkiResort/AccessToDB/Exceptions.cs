using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentAccessToDB
{
    public class ModelDBException : Exception
    {

        public ModelDBException(string exception_message)
        {
            this.exception_message = exception_message;
        }
        public string exception_message { get; }
    }

    public class UserDBException : ModelDBException
    {
        public UserDBException(string exception_message) : base(exception_message)
        {
        }
    }

    public class CardDBException : ModelDBException
    {
        public CardDBException(string exception_message) : base(exception_message)
        {
        }
    }
    public class CardReadingDBException : ModelDBException
    {
        public CardReadingDBException(string exception_message) : base(exception_message)
        {
        }
    }
    public class LiftDBException : ModelDBException
    {
        public LiftDBException(string exception_message) : base(exception_message)
        {
        }
    }
    public class SlopeDBException : ModelDBException
    {
        public SlopeDBException(string exception_message) : base(exception_message)
        {
        }
    }
    public class LiftSlopeDBException : ModelDBException
    {
        public LiftSlopeDBException(string exception_message) : base(exception_message)
        {
        }
    }
    public class MessageDBException : ModelDBException
    {
        public MessageDBException(string exception_message) : base(exception_message)
        {
        }
    }
    public class TurnstileDBException : ModelDBException
    {
        public TurnstileDBException(string exception_message) : base(exception_message)
        {
        }
    }
}
