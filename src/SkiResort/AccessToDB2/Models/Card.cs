
namespace AccessToDB2.Models
{
    public class Card
    {
        public Card(int cardID, int activationTime, string type)
        {
            CardID = cardID;
            ActivationTime = activationTime;
            Type = type;
        }

        public int CardID { get; set; }
        public int ActivationTime { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Card> Cards { get; set; }

    }
}

