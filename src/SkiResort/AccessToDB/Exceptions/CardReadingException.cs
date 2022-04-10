namespace AccessToDB.Exceptions
{
    public class CardReadingException : Exception
    {

        public CardReadingException() : base() { }
        public CardReadingException(string? message) : base(message) { }
        public CardReadingException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}

