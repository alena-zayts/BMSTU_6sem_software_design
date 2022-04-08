namespace BL.Exceptions
{
    public class PermissionsException : Exception
    {
        public uint? UserID { get; }
        public string? FunctionName { get; }

        public PermissionsException() : base() { }
        public PermissionsException(string? message) : base(message) { }
        public PermissionsException(string? message, Exception? innerException) : base(message, innerException) { }

        public PermissionsException(string? message, uint? userID, string? functionName) : base(message)
        {
            this.UserID = userID;
            this.FunctionName = functionName;
        }
    }
}
