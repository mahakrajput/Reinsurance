using System;
using System.Collections.Generic;
using Reinsurance.Model;

namespace Reinsurance.Evaluation
{
    public class Evaluator
    {
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

        }
    }
}
