using BL.Models;

namespace BL.Exceptions
{
    public class UserException : Exception
    {
        public User? User { get; }

        public UserException(): base() { }
        public UserException(string? message) : base(message) { }
        public UserException(string? message, Exception? innerException) : base(message, innerException) { }

        public UserException(string? message, User? user) : base(message)
        {
            this.User = user;
        }
    }
}
