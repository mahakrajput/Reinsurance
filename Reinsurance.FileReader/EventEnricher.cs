using Reinsurance.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Reinsurance.FileReader
{
    public class EventEnricher
    {
        private static readonly int MaxPerilValue;
        private static readonly int MaxRegionValue;

        static EventEnricher()
        {
            MaxPerilValue = (int)Enum.GetValues(typeof(Peril)).Cast<Peril>().Max();
            MaxRegionValue = (int)Enum.GetValues(typeof(Region)).Cast<Region>().Max();
        }

        public static List<Event> GetEnrichedEvents(int[][] data)
        {
            if (data == null || data.GetLength(0) <= 0)
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

            return events;
        }


        private static Event GetEventFromData(int[] data)
        {
            if(data.Length != 4)
            {
                return null;
            }

            if(data[1] > MaxPerilValue || data[2] > MaxRegionValue)
            {
                return null;
            }

            return new Event(data[0], (Peril)data[1], (Region)data[2], data[3]);
        }
    }
}
