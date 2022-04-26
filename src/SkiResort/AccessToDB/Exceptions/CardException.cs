namespace AccessToDB.Exceptions
{
    public class CardException : Exception
    { 
        public CardException() : base() { }
        public CardException(string? message) : base(message) { }
        public CardException(string? message, Exception? innerException) : base(message, innerException) { }
    }

    public class CardNotFoundException: CardException
    {
        public uint? cardID;
        public CardNotFoundException(uint cardID)
        {
            this.cardID = cardID;
        }
    }

    public class CardAddingException: CardException
    {
        public uint? cardID;
        public DateTimeOffset? activationTime;
        public string? type;
        public CardAddingException(uint cardID, DateTimeOffset activationTime, string type)
        {
            this.cardID = cardID;
            this.activationTime = activationTime;
            this.type = type;
        }
    }

    public class CardAddingAutoIncrementException: CardException
    {
        public DateTimeOffset? activationTime;
        public string? type;
        public CardAddingAutoIncrementException(DateTimeOffset activationTime, string type)
        {
            this.activationTime = activationTime;
            this.type = type;
        }
    }
    public class CardUpdateException: CardException
    {
        public uint? cardID;
        public DateTimeOffset? activationTime;
        public string? type;
        public CardUpdateException(uint cardID, DateTimeOffset newActivationTime, string newType)
        {
            this.cardID = cardID;
            this.activationTime = newActivationTime;
            this.type = newType;
        }
    }
    public class CardDeleteException: CardException
    {
        public uint? cardID;
        public CardDeleteException(uint cardID)
        {
            this.cardID = cardID;
        }
    }
}

