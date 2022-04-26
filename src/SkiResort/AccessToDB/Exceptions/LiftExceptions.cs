namespace AccessToDB.Exceptions
{
    public class LiftExceptions : Exception
    {

        public LiftExceptions() : base() { }
        public LiftExceptions(string? message) : base(message) { }
        public LiftExceptions(string? message, Exception? innerException) : base(message, innerException) { }

    }
    public class LiftNotFoundException: LiftExceptions
    {
        public LiftNotFoundException() : base() { }
        public LiftNotFoundException(string? message) : base(message) { }
        public LiftNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class LiftAddException: LiftExceptions
    {
        public LiftAddException() : base() { }
        public LiftAddException(string? message) : base(message) { }
        public LiftAddException(string? message, Exception? innerException) : base(message, innerException) { }


    }

    public class LiftAddAutoIncrementException: LiftExceptions
    {
        public LiftAddAutoIncrementException() : base() { }
        public LiftAddAutoIncrementException(string? message) : base(message) { }
        public LiftAddAutoIncrementException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class LiftUpdateException: LiftExceptions
    {
        public LiftUpdateException() : base() { }
        public LiftUpdateException(string? message) : base(message) { }
        public LiftUpdateException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class LiftDeleteException: LiftExceptions
    {
        public LiftDeleteException() : base() { }
        public LiftDeleteException(string? message) : base(message) { }
        public LiftDeleteException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}

