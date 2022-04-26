using BL.Models;

namespace BL.Exceptions
{
    public class LiftSlopeExceptions : Exception
    {
        public LiftSlope? LiftSlope { get; }

        public LiftSlopeExceptions() : base() { }
        public LiftSlopeExceptions(string? message) : base(message) { }
        public LiftSlopeExceptions(string? message, Exception? innerException) : base(message, innerException) { }

        public LiftSlopeExceptions(string? message, LiftSlope? liftSlope) : base(message)
        {
            this.LiftSlope = liftSlope;
        }
    }
}
