using System;
namespace Reinsurance.Model
{
    public class Event
    {
        public Event(int id, Peril peril, Region region, int eventLoss)
        {
            Id = id;
            Peril = peril;
            Region = region;
            EventLoss = eventLoss;
        }

        public int Id { private set; get; }
        public Peril Peril { private set; get; }
        public Region Region { private set; get; }
        public int EventLoss { private set; get; }
        public int TotalLoss { private set; get; }
    }
}
