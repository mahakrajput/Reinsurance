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
        public int TotalLoss { set; get; }

        public override string ToString()
        {
            return string.Format("{0} happened in {1}, with loss incurred {2}",
            Peril, Region, EventLoss);
        }

        public string PrintReinsuranceTotalLoss()
        {
            return string.Format("{0} happened in {1}, company's total loss is {2}",
            Peril, Region, TotalLoss);
        }
    }
}
