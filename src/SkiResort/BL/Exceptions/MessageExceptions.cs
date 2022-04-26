using BL.Models;

namespace BL.Exceptions
{
    public class MessageExceptions : Exception
    {
        public Message? MessageModel { get; }

        public MessageExceptions() : base() { }
        public MessageExceptions(string? message) : base(message) { }
        public MessageExceptions(string? message, Exception? innerException) : base(message, innerException) { }

        public MessageExceptions(string? message, Message? messageModel) : base(message)
        {
            this.MessageModel = messageModel;
        }
    }
    public class MessageCheckingException: MessageExceptions
    {
        public MessageCheckingException() : base() { }
        public MessageCheckingException(string? message) : base(message) { }
        public MessageCheckingException(string? message, Exception? innerException) : base(message, innerException) { }

        public MessageCheckingException(string? message, Message? messageModel) : base(message, messageModel)
        {
        }

    }
    public class MessageCreationException: MessageExceptions
    {
        public MessageCreationException() : base() { }
        public MessageCreationException(string? message) : base(message) { }
        public MessageCreationException(string? message, Exception? innerException) : base(message, innerException) { }

        public MessageCreationException(string? message, Message? messageModel) : base(message, messageModel)
        {
        }

    }
}
