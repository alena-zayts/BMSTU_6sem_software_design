using BL.Models;

namespace BL.Exceptions
{
    public class TurnstileException : Exception
    {
        public Turnstile? Turnstile { get; }

        public TurnstileException() : base() { }
        public TurnstileException(string? message) : base(message) { }
        public TurnstileException(string? message, Exception? innerException) : base(message, innerException) { }

        public TurnstileException(string? message, Turnstile? turnstile) : base(message)
        {
            this.Turnstile = turnstile;
        }
    }
}
