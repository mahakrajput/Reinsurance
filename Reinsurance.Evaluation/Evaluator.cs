using System;
using System.Linq;
using System.Collections.Generic;
using Reinsurance.Model;

namespace Reinsurance.Evaluation
{
    public class Evaluator
    {
        private List<Deal> Deals = null;
        private static Evaluator instance;

        private Evaluator() { }

        public static Evaluator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Evaluator();
                }
                return instance;
            }
        }

        public void EvaluateLoss(List<Deal> deals, List<Event> events)
        {
            if (!deals.Any())
            {
                Console.WriteLine("No deals available to evaluate loss");
                return;
            }

            if (!events.Any())
            {
                Console.WriteLine("No events available to evaluate loss");
                return;
            }

            Deals = deals;
            foreach (Event catastrophe in events)
            {
                var dealIds = GetCorrespondingDealsForAPeril(catastrophe.Peril);

                if (!dealIds.Any())
                {
                    continue;
                }

                CalculateTotalLossForAnEvent(catastrophe, dealIds);

            }
        }


        private List<int> GetCorrespondingDealsForAPeril(Peril peril)
        {
            return Deals.Where(d => d.Perils.Contains(peril)).Select(deal => deal.DealId).ToList();
        }


        private int CalculateLoss(int eventLoss, int dealRetention, int dealLimit)
        {
            var loss = 0;

            if (eventLoss > dealLimit)
            {
                loss = dealLimit;
            }
            else
            {
                loss = dealRetention;
            }

            return loss;
        }

        private void CalculateTotalLossForAnEvent(Event e, List<int> dealIds)
        {
            var filteredDeals = Deals.Where(d => dealIds.Contains(d.DealId));
            int totalLoss = 0;

            foreach (Deal deal in filteredDeals)
            {
                if (deal.Regions.Contains(e.Region))
                {
                    totalLoss += CalculateLoss(e.EventLoss, deal.Retention, deal.Limit);
                }
            }

            e.TotalLoss = totalLoss;
        }
    }
}
