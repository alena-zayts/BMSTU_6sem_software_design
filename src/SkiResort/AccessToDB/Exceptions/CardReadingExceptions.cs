namespace AccessToDB.Exceptions
{
    public class CardReadingExceptions : Exception
    {

        public CardReadingExceptions() : base() { }
        public CardReadingExceptions(string? message) : base(message) { }
        public CardReadingExceptions(string? message, Exception? innerException) : base(message, innerException) { }

    }
    public class CountCardReadingsException: CardReadingExceptions
    {
        public CountCardReadingsException() : base() { }
        public CountCardReadingsException(string? message) : base(message) { }
        public CountCardReadingsException(string? message, Exception? innerException) : base(message, innerException) { }

    }
    public class CardReadingAddAutoIncrementException: CardReadingExceptions
    {
        public CardReadingAddAutoIncrementException() : base() { }
        public CardReadingAddAutoIncrementException(string? message) : base(message) { }
        public CardReadingAddAutoIncrementException(string? message, Exception? innerException) : base(message, innerException) { }

    }
    public class CardReadingAddException: CardReadingExceptions
    {
        public CardReadingAddException() : base() { }
        public CardReadingAddException(string? message) : base(message) { }
        public CardReadingAddException(string? message, Exception? innerException) : base(message, innerException) { }

    }
    public class CardReadingDeleteException: CardReadingExceptions
    {
        public CardReadingDeleteException() : base() { }
        public CardReadingDeleteException(string? message) : base(message) { }
        public CardReadingDeleteException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}

