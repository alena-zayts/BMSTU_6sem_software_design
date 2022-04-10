namespace AccessToDB.Exceptions
{
    public class LiftSlopeException : Exception
    {

        public LiftSlopeException() : base() { }
        public LiftSlopeException(string? message) : base(message) { }
        public LiftSlopeException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
