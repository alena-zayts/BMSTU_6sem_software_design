using BL.Models;

namespace BL.Exceptions
{
    public class CardException : Exception
    {
        public Card? Card { get; }

        public CardException() : base() { }
        public CardException(string? message) : base(message) { }
        public CardException(string? message, Exception? innerException) : base(message, innerException) { }

        public CardException(string? message, Card? card)
        {
            this.Card = card;
        }
    }
}

