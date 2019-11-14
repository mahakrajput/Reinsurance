using Reinsurance.Model;
using System.Collections.Generic;
using System.Linq;

namespace Reinsurance.FileReader
{
    public class EventEnricher
    {

        public static List<Event> GetSortedAndEnrichedEvents(int[][] data)
        {
            if(data.GetLength(0) <= 0)
            {
                return new List<Event>();
            }

            var events = new List<Event>();
            Event eventResult = null;

            for (int index = 0; index < data.GetLength(0); index++)
            {
                eventResult = GetEventFromData(data[index]);

                if(eventResult != null)
                {
                    events.Add(eventResult);
                }
            }

            if (events.Any())
            {
                var sortedEvents = events.OrderBy(e => e.Id).ToList();
                return sortedEvents;
            }

            return events;
        }

        private static Event GetEventFromData(int[] data)
        {
            if(data.Length != 4)
            {
                return null;
            }

            return new Event(data[0], (Peril)data[1], (Region)data[2], data[3]);
        }
    }
}
