namespace AccessToDB.Exceptions
{
    public class MessageException : Exception
    {

        public MessageException() : base() { }
        public MessageException(string? message) : base(message) { }
        public MessageException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}
