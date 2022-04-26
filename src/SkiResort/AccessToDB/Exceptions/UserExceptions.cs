namespace AccessToDB.Exceptions
{
    public class UserExceptions : Exception
    {
        public UserExceptions(): base() { }
        public UserExceptions(string? message) : base(message) { }
        public UserExceptions(string? message, Exception? innerException) : base(message, innerException) { }

    }
    public class UserNotFoundException: UserExceptions
    {
        public UserNotFoundException() : base() { }
        public UserNotFoundException(string? message) : base(message) { }
        public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class UserDeleteException: UserExceptions
    {
        public UserDeleteException() : base() { }
        public UserDeleteException(string? message) : base(message) { }
        public UserDeleteException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class UserUpdateException: UserExceptions
    {
        public UserUpdateException() : base() { }
        public UserUpdateException(string? message) : base(message) { }
        public UserUpdateException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class UserAddAutoIncrementException: UserExceptions
    {
        public UserAddAutoIncrementException() : base() { }
        public UserAddAutoIncrementException(string? message) : base(message) { }
        public UserAddAutoIncrementException(string? message, Exception? innerException) : base(message, innerException) { }

    }

    public class UserAddException: UserExceptions
    {
        public UserAddException() : base() { }
        public UserAddException(string? message) : base(message) { }
        public UserAddException(string? message, Exception? innerException) : base(message, innerException) { }

    }

}
