namespace AccessToDB.Exceptions
{
    public class TurnstileException : Exception
    {
        public TurnstileException() : base() { }
        public TurnstileException(string? message) : base(message) { }
        public TurnstileException(string? message, Exception? innerException) : base(message, innerException) { }
    }

    public class TurnstileNotFoundException: TurnstileException
    {

    }

    public class TurnstileUpdateException: TurnstileException
    {
        
    }

    public class TurnstileDeleteException: TurnstileException
    {
        
    }

     public class TurnstileAddAutoIncrementException: TurnstileException
    {
        
    }

    public class TurnstileAddException: TurnstileException
    {
        
    }
}
