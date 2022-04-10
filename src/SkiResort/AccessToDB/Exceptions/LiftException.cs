namespace AccessToDB.Exceptions
{
    public class LiftException : Exception
    {

        public LiftException() : base() { }
        public LiftException(string? message) : base(message) { }
        public LiftException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}

