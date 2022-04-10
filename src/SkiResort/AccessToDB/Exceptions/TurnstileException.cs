namespace AccessToDB.Exceptions
{
    public class TurnstileException : Exception
    {
        public TurnstileException() : base() { }
        public TurnstileException(string? message) : base(message) { }
        public TurnstileException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
