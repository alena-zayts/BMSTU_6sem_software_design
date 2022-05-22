namespace AccessToDB2.Models
{
    public class Lift
    {
        public Lift(int liftID, string liftName, bool isOpen, int seatsAmount, int liftingTime, int queueTime)
        {
            LiftID = liftID;
            LiftName = liftName;
            IsOpen = isOpen;
            SeatsAmount = seatsAmount;
            LiftingTime = liftingTime;
            QueueTime = queueTime;
        }

        public int LiftID { get; set; }
        public string LiftName { get; set; }
        public bool IsOpen { get; set; }
        public int SeatsAmount { get; set; }
        public int LiftingTime { get; set; }
        public int QueueTime { get; set; }

        public virtual ICollection<Lift> Lifts { get; set; }

    }
}

