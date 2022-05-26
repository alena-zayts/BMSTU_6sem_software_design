using BL.Exceptions.MessageExceptions;

namespace AccessToDB2.Models
{
    public class Message
    {
        public Message() { }
        public Message(int messageID, int senderID, int checkedByID, string text)
        {
            MessageId = messageID;
            SenderId = senderID;
            CheckedById = checkedByID;
            Text = text;
        }

        public int MessageId { get; set; }
        public int? SenderId { get; set; }
        public int? CheckedById { get; set; }
        public string? Text { get; set; }

    }
}

