namespace AccessToDB.Exceptions
{
    public class TurnstileExceptions : Exception
    {
        public TurnstileExceptions() : base() { }
        public TurnstileExceptions(string? message) : base(message) { }
        public TurnstileExceptions(string? message, Exception? innerException) : base(message, innerException) { }
    
    }

    public class TurnstileNotFoundException: TurnstileExceptions
    {
        public TurnstileNotFoundException() : base() { }
        public TurnstileNotFoundException(string? message) : base(message) { }
        public TurnstileNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class TurnstileUpdateException: TurnstileExceptions
    {
        public TurnstileUpdateException() : base() { }
        public TurnstileUpdateException(string? message) : base(message) { }
        public TurnstileUpdateException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class TurnstileDeleteException: TurnstileExceptions
    {
        public TurnstileDeleteException() : base() { }
        public TurnstileDeleteException(string? message) : base(message) { }
        public TurnstileDeleteException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class TurnstileAddAutoIncrementException: TurnstileExceptions
    {
        public TurnstileAddAutoIncrementException() : base() { }
        public TurnstileAddAutoIncrementException(string? message) : base(message) { }
        public TurnstileAddAutoIncrementException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class TurnstileAddException: TurnstileExceptions
    {
        public TurnstileAddException() : base() { }
        public TurnstileAddException(string? message) : base(message) { }
        public TurnstileAddException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}
