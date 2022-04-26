namespace AccessToDB.Exceptions
{
    public class CardExceptions : Exception
    { 
        public CardExceptions() : base() { }
        public CardExceptions(string? message) : base(message) { }
        public CardExceptions(string? message, Exception? innerException) : base(message, innerException) { }
    }

    public class CardNotFoundException: CardExceptions
    {
        public uint? cardID;
        public CardNotFoundException(uint cardID)
        {
            this.cardID = cardID;
        }
    }

    public class CardAddingException: CardExceptions
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

    public class CardAddingAutoIncrementException: CardExceptions
    {
        public DateTimeOffset? activationTime;
        public string? type;
        public CardAddingAutoIncrementException(DateTimeOffset activationTime, string type)
        {
            this.activationTime = activationTime;
            this.type = type;
        }
    }
    public class CardUpdateException: CardExceptions
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
    public class CardDeleteException: CardExceptions
    {
        public uint? cardID;
        public CardDeleteException(uint cardID)
        {
            this.cardID = cardID;
        }
    }
}

