
namespace BL.Models
{
    public class Card
    {
        public uint CardID { get; }
        public uint ActivationTime { get; }
        public string Type { get; }

        public Card(uint cardID, uint activationTime, string type)
        {
            this.CardID = cardID;
            this.ActivationTime = activationTime;
            this.Type = type;

        }

        public override bool Equals(object? obj)
        {
            return obj is Card dB &&
                   CardID == dB.CardID &&
                   ActivationTime == dB.ActivationTime &&
                   Type == dB.Type;
        }
    }
}

