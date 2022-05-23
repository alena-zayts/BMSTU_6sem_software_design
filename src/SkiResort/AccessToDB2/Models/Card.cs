
namespace AccessToDB2.Models
{
    public class Card
    {
        public Card() { }
        public Card(int cardID, int activationTime, string type)
        {
            CardId = cardID;
            ActivationTime = activationTime;
            Type = type;
        }

        public int CardId { get; set; }
        public int? ActivationTime { get; set; }
        public string? Type { get; set; }

    }
}

