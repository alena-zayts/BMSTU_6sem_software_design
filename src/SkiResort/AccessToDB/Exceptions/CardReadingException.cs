namespace AccessToDB.Exceptions
{
    public class CardReadingException : Exception
    {

        public CardReadingException() : base() { }
        public CardReadingException(string? message) : base(message) { }
        public CardReadingException(string? message, Exception? innerException) : base(message, innerException) { }

    }
    public class CountCardReadingsException: CardReadingException
    {

    }
    public class CardReadingAddAutoIncrementException: CardReadingException
    {

    }
    public class CardReadingAddException: CardReadingException
    {

    }
    public class CardReadingDeleteException: CardReadingException
    {

    }
}

