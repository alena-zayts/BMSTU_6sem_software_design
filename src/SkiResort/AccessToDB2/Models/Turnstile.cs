namespace AccessToDB2.Models
{
    public class Turnstile
    {
        public Turnstile(int turnstileID, int liftID, bool isOpen)
        {
            TurnstileID = turnstileID;
            LiftID = liftID;
            IsOpen = isOpen;
        }

        public int TurnstileID { get; set; }
        public int LiftID { get; set; }
        public bool IsOpen { get; set; }
        public virtual ICollection<Turnstile> Turnstiles { get; set; }
    }
}

