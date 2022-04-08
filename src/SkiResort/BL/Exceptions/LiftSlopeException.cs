using BL.Models;

namespace BL.Exceptions
{
    public class LiftSlopeException : Exception
    {
        public LiftSlope? LiftSlope { get; }

        public LiftSlopeException() : base() { }
        public LiftSlopeException(string? message) : base(message) { }
        public LiftSlopeException(string? message, Exception? innerException) : base(message, innerException) { }

        public LiftSlopeException(string? message, LiftSlope? liftSlope) : base(message)
        {
            this.LiftSlope = liftSlope;
        }
    }
}
