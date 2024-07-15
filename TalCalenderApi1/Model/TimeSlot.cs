namespace TalCalenderApi1.Model
{
    public class TimeSlot
    {
        public int SlotId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool Available { get; set; }
    }

}
