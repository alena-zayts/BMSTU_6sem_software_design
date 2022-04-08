using BL.Models;

namespace BL.Exceptions
{
    public class MessageException : Exception
    {
        public Message? MessageModel { get; }

        public MessageException() : base() { }
        public MessageException(string? message) : base(message) { }
        public MessageException(string? message, Exception? innerException) : base(message, innerException) { }

        public MessageException(string? message, Message? messageModel) : base(message)
        {
            this.MessageModel = messageModel;
        }
    }
}
