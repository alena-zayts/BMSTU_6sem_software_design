namespace AccessToDB.Exceptions
{
    public class LiftSlopeExceptions : Exception
    {

        public LiftSlopeExceptions() : base() { }
        public LiftSlopeExceptions(string? message) : base(message) { }
        public LiftSlopeExceptions(string? message, Exception? innerException) : base(message, innerException) { }
    
    }
    
    public class LiftSlopeNotFoundException: LiftSlopeExceptions
    {
        public LiftSlopeNotFoundException() : base() { }
        public LiftSlopeNotFoundException(string? message) : base(message) { }
        public LiftSlopeNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }


    }
    public class LiftSlopeDeleteException: LiftSlopeExceptions
    {
        public LiftSlopeDeleteException() : base() { }
        public LiftSlopeDeleteException(string? message) : base(message) { }
        public LiftSlopeDeleteException(string? message, Exception? innerException) : base(message, innerException) { }


    }
    public class LiftSlopeUpdateException: LiftSlopeExceptions
    {
        public LiftSlopeUpdateException() : base() { }
        public LiftSlopeUpdateException(string? message) : base(message) { }
        public LiftSlopeUpdateException(string? message, Exception? innerException) : base(message, innerException) { }


    }
    public class LiftSlopeAddAutoIncrementException: LiftSlopeExceptions
    {
        public LiftSlopeAddAutoIncrementException() : base() { }
        public LiftSlopeAddAutoIncrementException(string? message) : base(message) { }
        public LiftSlopeAddAutoIncrementException(string? message, Exception? innerException) : base(message, innerException) { }


    }
    public class LiftSlopeAddException: LiftSlopeExceptions
    {
        public LiftSlopeAddException() : base() { }
        public LiftSlopeAddException(string? message) : base(message) { }
        public LiftSlopeAddException(string? message, Exception? innerException) : base(message, innerException) { }


    }
    public class LiftSlopeSlopeNotFoundException: LiftSlopeExceptions
    {

        public LiftSlopeSlopeNotFoundException() : base() { }
        public LiftSlopeSlopeNotFoundException(string? message) : base(message) { }
        public LiftSlopeSlopeNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }


    }
    public class LiftSlopeLiftNotFoundException: LiftSlopeExceptions
    {

        public LiftSlopeLiftNotFoundException() : base() { }
        public LiftSlopeLiftNotFoundException(string? message) : base(message) { }
        public LiftSlopeLiftNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}
