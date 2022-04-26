namespace AccessToDB.Exceptions
{
    public class UserException : Exception
    {
        public UserException(): base() { }
        public UserException(string? message) : base(message) { }
        public UserException(string? message, Exception? innerException) : base(message, innerException) { }

    }
    public class UserNotFoundException: UserException
    {

    }

    public class UserDeleteException: UserException
    {
        
    }

    public class UserUpdateException: UserException
    {
        
    }

    public class UserAddAutoIncrementException: UserException
    {
        
    }

    public class UserAddException: UserException
    {
        
    }

}
