using NUnit.Framework;
using Reinsurance.FileReader;
using System.Linq;
using Reinsurance.Model;

namespace Reinsurance.Tests
{
    [TestFixture]
    public class EventEnricherTest
    {
        [Test]
        public void Test_NoEventIsCreated_When_InputIsNUll()
        {
            var events = EventEnricher.GetEnrichedEvents(null);
            Assert.AreEqual(events.Count, 0);
        }

        [Test]
        public void Test_NoEventIsCreated_When_InputIsEmptyArray()
        {
            var events = EventEnricher.GetEnrichedEvents(new int[0][]);
            Assert.AreEqual(events.Count, 0);
        }

        [Test]
        public void Test_NoEventIsCreated_When_InputIsLessThan4()
        {
            var input = new[] {new []{3, 3, 3} };

            var events = EventEnricher.GetEnrichedEvents(input);
            Assert.AreEqual(events.Count, 0);
        }

        [Test]
        public void Test_NoEventIsCreated_When_InputIsGreaterThan4()
        {
            var input = new[] { new[] { 3, 3, 3, 750,88 } };

            var events = EventEnricher.GetEnrichedEvents(input);
            Assert.AreEqual(events.Count, 0);
        }

        [Test]
        public void Test_EventIsCreated_When_InputIsAsExpectedForAnEvent()
        {
            var input = new[] { new[] { 3, 3, 3, 750} };

            var events = EventEnricher.GetEnrichedEvents(input);
            Assert.AreEqual(events.Count, 1);
            var e = events.FirstOrDefault();
            Assert.AreEqual(e.Id, 3);
            Assert.AreEqual(e.Peril, Peril.Flood);
            Assert.AreEqual(e.Region, Region.Florida);
            Assert.AreEqual(e.EventLoss, 750);

        }

        [Test]
        public void Test_EventsAreCreated_When_MultipleInputsAreProvided()
        {
            var input = new[] { new[] { 3, 3, 3, 750 }, new[] { 2, 2, 1, 1000 } };

            var events = EventEnricher.GetEnrichedEvents(input);
            Assert.AreEqual(events.Count, 2);
        }

        [Test]
        public void Test_NoEventIsCreated_When_PerilValueIsNotInTheEnum()
        {
            var input = new[] { new[] { 3, 4, 3, 750 } };

            var events = EventEnricher.GetEnrichedEvents(input);
            Assert.AreEqual(events.Count, 0);
        }

        [Test]
        public void Test_NoEventIsCreated_When_RegionValueIsNotInTheEnum()
        {
            var input = new[] { new[] { 3, 3, 4, 750 } };

            var events = EventEnricher.GetEnrichedEvents(input);
            Assert.AreEqual(events.Count, 0);
        }

        [Test]
        public void Test_NoEventIsCreated_When_PerilAndRegionValuesAreNotInTheEnum()
        {
            var input = new[] { new[] { 3, 4, 4, 750 } };

            var events = EventEnricher.GetEnrichedEvents(input);
            Assert.AreEqual(events.Count, 0);
        }
    }
}
