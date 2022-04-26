using BL.Models;

namespace BL.Exceptions
{
    public class UserExceptions : Exception
    {
        public User? User { get; }

        public UserExceptions(): base() { }
        public UserExceptions(string? message) : base(message) { }
        public UserExceptions(string? message, Exception? innerException) : base(message, innerException) { }

        public UserExceptions(string? message, User? user) : base(message)
        {
            this.User = user;
        }
    }
    public class UserDuplicateException: UserExceptions
    {
        public UserDuplicateException() : base() { }
        public UserDuplicateException(string? message) : base(message) { }
        public UserDuplicateException(string? message, Exception? innerException) : base(message, innerException) { }

        public UserDuplicateException(string? message, User? user) : base(message, user)
        {
        }
    }

    public class UserRegistrationException: UserExceptions
    {
        public UserRegistrationException() : base() { }
        public UserRegistrationException(string? message) : base(message) { }
        public UserRegistrationException(string? message, Exception? innerException) : base(message, innerException) { }

        public UserRegistrationException(string? message, User? user) : base(message, user)
        {
        }
    }

    public class UserAuthorizationException: UserExceptions
    {
        public UserAuthorizationException() : base() { }
        public UserAuthorizationException(string? message) : base(message) { }
        public UserAuthorizationException(string? message, Exception? innerException) : base(message, innerException) { }

        public UserAuthorizationException(string? message, User? user) : base(message, user)
        {
        }
    } 
}

