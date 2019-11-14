using System;
using System.Collections.Generic;
namespace Reinsurance.Model
{
    public class Deal
    {
        public Deal(int id, int retention, int limit, List<Peril> perils, List<Region> regions)
        {
            DealId = id;
            Retention = retention;
            Limit = limit;
            Perils = perils;
            Regions = regions;
        }

        public int DealId { private set; get; }
        public int Retention { private set; get; }
        public int Limit { private set; get; }
        public List<Peril> Perils;
        public List<Region> Regions;
    }
}
