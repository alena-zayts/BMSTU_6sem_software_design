namespace AccessToDB2.Models
{
    public class Turnstile
    {
        public Turnstile() { }
        public Turnstile(int turnstileID, int liftID, bool isOpen)
        {
            TurnstileId = turnstileID;
            LiftId = liftID;
            IsOpen = isOpen;
        }

        public int TurnstileId { get; set; }
        public int? LiftId { get; set; }
        public bool? IsOpen { get; set; }
    }
}

