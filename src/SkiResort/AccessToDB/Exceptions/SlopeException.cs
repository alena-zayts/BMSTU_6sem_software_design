namespace AccessToDB.Exceptions
{
    public class SlopeException : Exception
    {
        public SlopeException() : base() { }
        public SlopeException(string? message) : base(message) { }
        public SlopeException(string? message, Exception? innerException) : base(message, innerException) { }
    }

    public class SlopeNotFoundException: SlopeException
    {

    }

    public class SlopeAddException: SlopeException
    {
        
    }

    public class SlopeAddAutoIncrementException: SlopeException
    {
        
    }

    public class SlopeUpdateException: SlopeException
    {
        
    }

    public class SlopeDeleteException: SlopeException
    {
        
    } 
    
}
