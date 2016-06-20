using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TGCCaddy.Model;

namespace TGCCaddyTests
{
    [TestClass]
    public class ElevationAdjusterTests
    {
        private ElevationAdjuster adjuster;

        [TestInitialize]
        public void Setup()
        {
            adjuster = new ElevationAdjuster(0.27);
        }

        [TestMethod]
        public void positive_elevation_rounds_down_correctly()
        {
            adjuster.Elevation = 9;

            var result = adjuster.GetAdjustedDistance();

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void negative_elevation_rounds_down_correctly()
        {
            adjuster.Elevation = -9;

            var result = adjuster.GetAdjustedDistance();

            Assert.AreEqual(-2, result);
        }

        [TestMethod]
        public void positive_elevation_rounds_up_correctly()
        {
            adjuster.Elevation = 11;

            var result = adjuster.GetAdjustedDistance();

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void negative_elevation_rounds_up_correctly()
        {
            adjuster.Elevation = -11;

            var result = adjuster.GetAdjustedDistance();

            Assert.AreEqual(-3, result);
        }

    }
}
