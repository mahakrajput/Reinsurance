using System;
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
            var events = EventEnricher.GetSortedAndEnrichedEvents(Data.Events);
            var deals = DealLoader.GetDeals();

            //print deals
            //print events

            Evaluator.Instance.EvaluateLoss(deals, events);


            Console.WriteLine("Hello World!");

            Console.ReadLine();
        }
    }
}
