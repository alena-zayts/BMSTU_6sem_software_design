using BL.Exceptions.MessageExceptions;

namespace AccessToDB2.Models
{
    public class Message
    {
        public Message(int messageID, int senderID, int checkedByID, string text)
        {
            MessageID = messageID;
            SenderID = senderID;
            CheckedByID = checkedByID;
            Text = text;
        }

        public int MessageID { get; set; }
        public int SenderID { get; set; }
        public int CheckedByID { get; set; }
        public string Text { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

    }
}

