namespace AccessToDB.Exceptions
{
    public class MessageException : Exception
    {

        public MessageException() : base() { }
        public MessageException(string? message) : base(message) { }
        public MessageException(string? message, Exception? innerException) : base(message, innerException) { }

    }
    public class MessageNotFoundException: MessageException
    {

    }

    public class MessageDeleteException: MessageException
    {
        
    }

    public class MessageUpdateException: MessageException
    {
        
    }

    public class MessageAddAutoIncrementException: MessageException
    {
        
    }

    public class MessageAddException: MessageException
    {
        
    }
    
}
