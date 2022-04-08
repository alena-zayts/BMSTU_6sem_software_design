namespace BL.Models
{
    public class Lift
    {
        public uint LiftID { get; }
        public string LiftName { get; }
        public bool IsOpen { get; }
        public uint SeatsAmount { get; }
        public uint LiftingTime { get;  }
        public uint QueueTime { get;}
        public List<Slope>? ConnectedSlopes { get;}

        public Lift(uint liftID, string liftName, bool isOpen, uint seatsAmount, uint liftingTime, uint queueTime)
        {
            this.LiftID = liftID;
            this.LiftName = liftName;
            this.IsOpen = isOpen;
            this.SeatsAmount = seatsAmount;
            this.LiftingTime = liftingTime;
            this.QueueTime = queueTime;
        }

        public override bool Equals(object? obj)
        {
            return obj is Lift dB &&
                   LiftID == dB.LiftID &&
                   LiftName == dB.LiftName &&
                   IsOpen == dB.IsOpen &&
                   SeatsAmount == dB.SeatsAmount &&
                   LiftingTime == dB.LiftingTime &&
                   QueueTime == dB.QueueTime;
        }
    }
}

