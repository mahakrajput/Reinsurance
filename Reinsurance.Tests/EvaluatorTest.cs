using System;
using NUnit.Framework;
using Reinsurance.FileReader;
using System.Linq;
using Reinsurance.Model;
using Reinsurance.Evaluation;
using System.Collections.Generic;

namespace Reinsurance.Tests
{
    [TestFixture]
    public class EvaluatorTest
    {

        [Test]
        public void Test_Noexception_When_InputIsNull()
        {
            Exception ex = null;
            try
            {
                Evaluator.Instance.EvaluateLoss(null, null);
            }
            catch(Exception exception)
            {
                ex = exception;
            }
            finally
            {
                Assert.AreEqual(ex, null);
            }
        }

        [Test]
        public void Test_TotalLoss_IsEqual_ToDealLimit_When_EventLoss_IsGreaterThanLimit()
        {
            var dealLimit = 200;
            var dealRetention = 100;

            var deal = new Deal(1, dealRetention, dealLimit, new List<Peril>() { Peril.Earthquake}, new List<Region>() { Region.California});
            var e = new Event(1, Peril.Earthquake, Region.California, 1000);

            Evaluator.Instance.EvaluateLoss(new List<Deal>() { deal}, new List<Event>() { e });

            Assert.AreEqual(e.TotalLoss, dealLimit);
        }

        [Test]
        public void Test_TotalLoss_IsEqual_ToDealRetention_When_EventLoss_IsLessThanLimit()
        {
            var dealLimit = 200;
            var dealRetention = 100;

            var deal = new Deal(1, dealRetention, dealLimit, new List<Peril>() { Peril.Earthquake }, new List<Region>() { Region.California });
            var e = new Event(1, Peril.Earthquake, Region.California, 150);

            Evaluator.Instance.EvaluateLoss(new List<Deal>() { deal }, new List<Event>() { e });

            Assert.AreEqual(e.TotalLoss, dealRetention);
        }

        [Test]
        public void Test_TotalLoss_IsEqual_ToDealRetention_When_EventLoss_IsLessThanRetention()
        {
            var dealLimit = 200;
            var dealRetention = 100;

            var deal = new Deal(1, dealRetention, dealLimit, new List<Peril>() { Peril.Earthquake }, new List<Region>() { Region.California });
            var e = new Event(1, Peril.Earthquake, Region.California, 50);

            Evaluator.Instance.EvaluateLoss(new List<Deal>() { deal }, new List<Event>() { e });

            Assert.AreEqual(e.TotalLoss, dealRetention);
        }

        [Test]
        public void Test_TotalLoss_IsNotCalculated_When_CorrespondingDeal_DoesnotExists()
        {
            var dealLimit = 200;
            var dealRetention = 100;

            var deal = new Deal(1, dealRetention, dealLimit, new List<Peril>() { Peril.Flood }, new List<Region>() { Region.California });
            var e = new Event(1, Peril.Earthquake, Region.California, 150);

            Evaluator.Instance.EvaluateLoss(new List<Deal>() { deal }, new List<Event>() { e });

            Assert.AreEqual(e.TotalLoss, 0);
        }

        [Test]
        public void Test_TotalLoss_IsEqual_SumOfBothDealRetentions_When_EventLoss_IsLessThanBothDealLimits()
        {
            var dealLimit = 200;
            var dealRetention = 100;

            var dealLimit2 = 900;
            var dealRetention2 = 300;

            var deal = new Deal(1, dealRetention, dealLimit, new List<Peril>() { Peril.Earthquake }, new List<Region>() { Region.California });
            var deal2 = new Deal(1, dealRetention2, dealLimit2, new List<Peril>() { Peril.Earthquake }, new List<Region>() { Region.California, Region.Florida });

            var e = new Event(1, Peril.Earthquake, Region.California, 150);

            Evaluator.Instance.EvaluateLoss(new List<Deal>() { deal, deal2 }, new List<Event>() { e });

            Assert.AreEqual(e.TotalLoss, dealRetention + dealRetention2);
        }

        [Test]
        public void Test_TotalLoss_IsEqual_SumOfBothDealLimit_When_EventLoss_IsGreaterThanBothDealLimits()
        {
            var dealLimit = 200;
            var dealRetention = 100;

            var dealLimit2 = 900;
            var dealRetention2 = 300;

            var deal = new Deal(1, dealRetention, dealLimit, new List<Peril>() { Peril.Earthquake }, new List<Region>() { Region.California });
            var deal2 = new Deal(1, dealRetention2, dealLimit2, new List<Peril>() { Peril.Earthquake }, new List<Region>() { Region.California, Region.Florida });

            var e = new Event(1, Peril.Earthquake, Region.California, 1000);

            Evaluator.Instance.EvaluateLoss(new List<Deal>() { deal, deal2 }, new List<Event>() { e });

            Assert.AreEqual(e.TotalLoss, dealLimit + dealLimit2);
        }

        [Test]
        public void Test_TotalLoss_IsEqual_SumOfBothDealLimitAndRetention_When_EventLoss_IsGreaterThanDealLimit_AndLessThanOtherLimit()
        {
            var dealLimit = 200;
            var dealRetention = 100;

            var dealLimit2 = 900;
            var dealRetention2 = 300;

            var deal = new Deal(1, dealRetention, dealLimit, new List<Peril>() { Peril.Earthquake }, new List<Region>() { Region.California });
            var deal2 = new Deal(1, dealRetention2, dealLimit2, new List<Peril>() { Peril.Earthquake }, new List<Region>() { Region.California, Region.Florida });

            var e = new Event(1, Peril.Earthquake, Region.California, 500);

            Evaluator.Instance.EvaluateLoss(new List<Deal>() { deal, deal2 }, new List<Event>() { e });

            Assert.AreEqual(e.TotalLoss, dealLimit + dealRetention2);
        }
    }
}
