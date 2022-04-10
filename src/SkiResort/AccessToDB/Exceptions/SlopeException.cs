namespace AccessToDB.Exceptions
{
    public class SlopeException : Exception
    {
        public SlopeException() : base() { }
        public SlopeException(string? message) : base(message) { }
        public SlopeException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
