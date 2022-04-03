using System;

namespace ComponentBL.ModelsBL
{
    public class MessageBL
    {
        public MessageBL(uint message_id, uint sender_id, uint checked_by_id, string text)
        {
            this.message_id = message_id;
            this.sender_id = sender_id;
            this.checked_by_id = checked_by_id;
            this.text = text;

        }
        public uint message_id { get; }
        public uint sender_id { get; set; }
        public uint checked_by_id { get; set; }
        public string text { get; set; }

        public MessageBL((uint, uint, uint, string) user_tuple)
        {
            this.message_id = user_tuple.Item1;
            this.sender_id = user_tuple.Item2;
            this.checked_by_id = user_tuple.Item3;
            this.text = user_tuple.Item4;
        }

        public ValueTuple<uint, uint, uint, string> to_value_tuple()
        {
            return ValueTuple.Create(message_id, sender_id, checked_by_id, text);
        }

        public override bool Equals(object obj)
        {
            return obj is MessageBL dB &&
                   message_id == dB.message_id &&
                   sender_id == dB.sender_id &&
                   checked_by_id == dB.checked_by_id &&
                   text == dB.text;
        }
    }
}

