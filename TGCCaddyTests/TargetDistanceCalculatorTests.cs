using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TGCCaddy.Model;

namespace TGCCaddyTests
{

    [TestClass]
    public class TargetDistanceCalculatorTests
    {
        private Mock<IElevationAdjuster> elevationAdjuster;

        [TestInitialize]
        public void Setup()
        {
            elevationAdjuster = new Mock<IElevationAdjuster>();

            elevationAdjuster.Setup(x => x.GetAdjustedDistance()).Returns(5);
        }

        [TestMethod]
        public void set_elevation_sets_elevation_on_adjuster()
        {
            var distanceCalculator = new TargetDistanceCalculator(elevationAdjuster.Object);

            distanceCalculator.Elevation = 10;

            elevationAdjuster.VerifySet(x=>x.Elevation = 10);

        }

        [TestMethod]
        public void target_elevation_returns_correct_result()
        {
            var distanceCalculator = new TargetDistanceCalculator(elevationAdjuster.Object)
            {
                Distance = 10,
                Roll = 5
            };

            //distance should be 10 - 5 + 5 = 10;
            Assert.AreEqual(10, distanceCalculator.GetTargetDistance());
        }
    }
}
