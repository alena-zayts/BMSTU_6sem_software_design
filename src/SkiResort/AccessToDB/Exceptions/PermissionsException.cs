namespace AccessToDB.Exceptions
{
    public class PermissionsException : Exception
    {
        public PermissionsException() : base() { }
        public PermissionsException(string? message) : base(message) { }
        public PermissionsException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
