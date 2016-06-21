using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TGCCaddy.Model;

namespace TGCCaddyTests
{
    [TestClass]
    public class ShotCalculatorTests
    {
        private Mock<IClub> lobWedge;
        private Mock<IClub> sandWedge;
        private Mock<ITargetDistanceCalculator> distanceCalculator;
        private Mock<IWindAdjusterFactory> windAdjusterFactory;
        private Mock<IWindAdjuster> windAdjuster;

        [TestInitialize]
        public void Setup()
        {
            CreateLobWedge();

            CreateSandWedge();

            distanceCalculator = new Mock<ITargetDistanceCalculator>();
            distanceCalculator.Setup(x => x.GetTargetDistance()).Returns(0);
        }


        private void CreateSandWedge()
        {
            sandWedge = new Mock<IClub>();
            sandWedge.Setup(x => x.GetDistance(ShotType.Normal, 0)).Returns(95);
            sandWedge.Setup(x => x.GetDistance(ShotType.Normal, 1)).Returns(80);
            sandWedge.Setup(x => x.GetHalfDistance(ShotType.Normal, 0)).Returns(87);
            sandWedge.Setup(x => x.GetStepCountForShotType(ShotType.Normal)).Returns(2);

            sandWedge.Setup(x => x.GetDistance(ShotType.Pitch, 0)).Returns(55);
            sandWedge.Setup(x => x.GetDistance(ShotType.Pitch, 1)).Returns(47);
            sandWedge.Setup(x => x.GetHalfDistance(ShotType.Pitch, 0)).Returns(51);
            sandWedge.Setup(x => x.GetStepCountForShotType(ShotType.Normal)).Returns(2);

            sandWedge.Setup(x => x.GetDistance(ShotType.Flop, 0)).Returns(35);
            sandWedge.Setup(x => x.GetDistance(ShotType.Flop, 1)).Returns(26);
            sandWedge.Setup(x => x.GetHalfDistance(ShotType.Flop, 0)).Returns(30);
            sandWedge.Setup(x => x.GetStepCountForShotType(ShotType.Flop)).Returns(2);

            sandWedge.Setup(x => x.GetDistance(ShotType.Chip, 0)).Returns(11);
            sandWedge.Setup(x => x.GetDistance(ShotType.Chip, 1)).Returns(7);
            sandWedge.Setup(x => x.GetHalfDistance(ShotType.Chip, 0)).Returns(9);
            sandWedge.Setup(x => x.GetStepCountForShotType(ShotType.Chip)).Returns(2);
        }

        private void CreateLobWedge()
        {
            lobWedge = new Mock<IClub>();
            lobWedge.Setup(x => x.GetDistance(ShotType.Normal, 0)).Returns(75);
            lobWedge.Setup(x => x.GetDistance(ShotType.Normal, 1)).Returns(64);
            lobWedge.Setup(x => x.GetHalfDistance(ShotType.Normal, 0)).Returns(69);
            lobWedge.Setup(x => x.GetStepCountForShotType(ShotType.Normal)).Returns(2);

            lobWedge.Setup(x => x.GetDistance(ShotType.Pitch, 0)).Returns(45);
            lobWedge.Setup(x => x.GetDistance(ShotType.Pitch, 1)).Returns(39);
            lobWedge.Setup(x => x.GetHalfDistance(ShotType.Pitch, 0)).Returns(42);
            lobWedge.Setup(x => x.GetStepCountForShotType(ShotType.Normal)).Returns(2);

            lobWedge.Setup(x => x.GetDistance(ShotType.Flop, 0)).Returns(30);
            lobWedge.Setup(x => x.GetDistance(ShotType.Flop, 1)).Returns(24);
            lobWedge.Setup(x => x.GetHalfDistance(ShotType.Flop, 0)).Returns(27);
            lobWedge.Setup(x => x.GetStepCountForShotType(ShotType.Flop)).Returns(2);

            lobWedge.Setup(x => x.GetDistance(ShotType.Chip, 0)).Returns(7);
            lobWedge.Setup(x => x.GetDistance(ShotType.Chip, 1)).Returns(3);
            lobWedge.Setup(x => x.GetHalfDistance(ShotType.Chip, 0)).Returns(5);
            lobWedge.Setup(x => x.GetStepCountForShotType(ShotType.Chip)).Returns(2);
        }
    }
}
