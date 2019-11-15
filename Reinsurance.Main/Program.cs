using System;
using System.Linq;
using Reinsurance.FileReader;
using Reinsurance.Evaluation;
using System.Collections.Generic;
using Reinsurance.Model;

namespace Reinsurance.Main
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var events = EventEnricher.GetEnrichedEvents(Data.Events);
            var deals = DealLoader.GetDeals();


            deals.ForEach(d => Console.WriteLine(d));
            Console.WriteLine();
            Console.WriteLine();
            events.ForEach(d => Console.WriteLine(d));

            Evaluator.Instance.EvaluateLoss(deals, events);

            Console.WriteLine();
            Console.WriteLine();

            events.OrderBy(e => e.Id);
            events.ForEach(o => Console.WriteLine(o.PrintReinsuranceTotalLoss()));

            Console.ReadLine();
        }
    }
}
