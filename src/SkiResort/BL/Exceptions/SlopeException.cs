using BL.Models;

namespace BL.Exceptions
{
    public class SlopeException : Exception
    {
        public Slope? Slope { get; }

        public SlopeException() : base() { }
        public SlopeException(string? message) : base(message) { }
        public SlopeException(string? message, Exception? innerException) : base(message, innerException) { }

        public SlopeException(string? message, Slope? slope) : base(message)
        {
            this.Slope = slope;
        }
    }
}
