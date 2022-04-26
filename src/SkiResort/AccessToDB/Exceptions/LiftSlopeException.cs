namespace AccessToDB.Exceptions
{
    public class LiftSlopeException : Exception
    {

        public LiftSlopeException() : base() { }
        public LiftSlopeException(string? message) : base(message) { }
        public LiftSlopeException(string? message, Exception? innerException) : base(message, innerException) { }
    }
    
    public class LiftSlopeNotFoundException: LiftSlopeException
    {

    }
    public class LiftSlopeDeleteException: LiftSlopeException
    {
        
    }
    public class LiftSlopeUpdateException: LiftSlopeException
    {
        
    }
    public class LiftSlopeAddAutoIncrementException: LiftSlopeException
    {
        
    }
    public class LiftSlopeAddException: LiftSlopeException
    {
        
    }
    public class LiftSlopeSlopeNotFoundException: LiftSlopeException
    {
        
    }
    public class LiftSlopeLiftNotFoundException: LiftSlopeException
    {
        
    }
}
