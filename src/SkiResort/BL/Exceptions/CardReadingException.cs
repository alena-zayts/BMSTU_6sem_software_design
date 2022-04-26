using BL.Models;

namespace BL.Exceptions
{
    public class CardeadingException : Exception
    {
        public CardReading? CardReading { get; }

        public CardeadingException() : base() { }
        public CardeadingException(string? message) : base(message) { }
        public CardeadingException(string? message, Exception? innerException) : base(message, innerException) { }

        public CardeadingException(string? message, CardReading? cardReading): base(message)
        {
            this.CardReading = cardReading;
        }
    }
}

