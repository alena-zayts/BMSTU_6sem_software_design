namespace BL.Models
{
    public class Turnstile
    {
        public uint TurnstileID { get; }
        public uint LiftID { get; }
        public bool IsOpen { get; }

        public Turnstile(uint turnstileID, uint liftID, bool isOpen)
        {
            this.TurnstileID = turnstileID;
            this.LiftID = liftID;
            this.IsOpen = isOpen;

        }

        public override bool Equals(object? obj)
        {
            return obj is Turnstile dB &&
                   TurnstileID == dB.TurnstileID &&
                   LiftID == dB.LiftID &&
                   IsOpen == dB.IsOpen;
        }
    }
}

