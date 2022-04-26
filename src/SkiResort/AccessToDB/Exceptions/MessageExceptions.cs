namespace AccessToDB.Exceptions
{
    public class MessageExceptions : Exception
    {

        public MessageExceptions() : base() { }
        public MessageExceptions(string? message) : base(message) { }
        public MessageExceptions(string? message, Exception? innerException) : base(message, innerException) { }

    }
    public class MessageNotFoundException: MessageExceptions
    {
        public MessageNotFoundException() : base() { }
        public MessageNotFoundException(string? message) : base(message) { }
        public MessageNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class MessageDeleteException: MessageExceptions
    {
        public MessageDeleteException() : base() { }
        public MessageDeleteException(string? message) : base(message) { }
        public MessageDeleteException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class MessageUpdateException: MessageExceptions
    {
        public MessageUpdateException() : base() { }
        public MessageUpdateException(string? message) : base(message) { }
        public MessageUpdateException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class MessageAddAutoIncrementException: MessageExceptions
    {
        public MessageAddAutoIncrementException() : base() { }
        public MessageAddAutoIncrementException(string? message) : base(message) { }
        public MessageAddAutoIncrementException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class MessageAddException: MessageExceptions
    {
        public MessageAddException() : base() { }
        public MessageAddException(string? message) : base(message) { }
        public MessageAddException(string? message, Exception? innerException) : base(message, innerException) { }

    }

}
