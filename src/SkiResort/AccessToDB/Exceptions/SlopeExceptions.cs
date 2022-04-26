namespace AccessToDB.Exceptions
{
    public class SlopeExceptions : Exception
    {
        public SlopeExceptions() : base() { }
        public SlopeExceptions(string? message) : base(message) { }
        public SlopeExceptions(string? message, Exception? innerException) : base(message, innerException) { }
    }

    public class SlopeNotFoundException: SlopeExceptions
    {
        public SlopeNotFoundException() : base() { }
        public SlopeNotFoundException(string? message) : base(message) { }
        public SlopeNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
    }

    public class SlopeAddException: SlopeExceptions
    {
        public SlopeAddException() : base() { }
        public SlopeAddException(string? message) : base(message) { }
        public SlopeAddException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class SlopeAddAutoIncrementException: SlopeExceptions
    {
        public SlopeAddAutoIncrementException() : base() { }
        public SlopeAddAutoIncrementException(string? message) : base(message) { }
        public SlopeAddAutoIncrementException(string? message, Exception? innerException) : base(message, innerException) { }
    }

    public class SlopeUpdateException: SlopeExceptions
    {
        public SlopeUpdateException() : base() { }
        public SlopeUpdateException(string? message) : base(message) { }
        public SlopeUpdateException(string? message, Exception? innerException) : base(message, innerException) { }
    }

    public class SlopeDeleteException: SlopeExceptions
    {
        public SlopeDeleteException() : base() { }
        public SlopeDeleteException(string? message) : base(message) { }
        public SlopeDeleteException(string? message, Exception? innerException) : base(message, innerException) { }
    } 
    
}
