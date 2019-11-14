
using Reinsurance.Model;
using System.Collections.Generic;

namespace Reinsurance.FileReader
{
    public class DealLoader
    {

        public static List<Deal> GetDeals()
        {
            var deals = new List<Deal>
            {
                new Deal(1, 100, 500, new List<Peril> { Peril.Earthquake }, new List<Region> { Region.California }),
                new Deal(2, 1000, 3000, new List<Peril> { Peril.Hurricane }, new List<Region> { Region.Florida }),
                new Deal(3, 250, 250, new List<Peril> { Peril.Flood, Peril.Hurricane }, new List<Region> { Region.Florida, Region.Louisiana })
            };

            return deals;
        }

    }
}
