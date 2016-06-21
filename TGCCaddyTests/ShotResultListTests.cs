using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TGCCaddy.Model;

namespace TGCCaddyTests
{
    [TestClass]
    public class ShotResultListTests
    {
        private Mock<IShotResult> outOfRangeShot;
        private Mock<IShotResult> furtherOutOfRangeShot;
        private Mock<IShotResult> closestShot;
        private Mock<IShotResult> secondClosestShot;
        private Mock<IShotResult> thirdClosestShot;

        [TestInitialize]
        public void Setup()
        {
            outOfRangeShot = new Mock<IShotResult>();
            outOfRangeShot.Setup(x => x.DistanceToTarget).Returns(5);
            outOfRangeShot.Setup(x => x.IsWithinRange).Returns(false);       
     
            furtherOutOfRangeShot = new Mock<IShotResult>();
            furtherOutOfRangeShot.Setup(x => x.DistanceToTarget).Returns(10);
            furtherOutOfRangeShot.Setup(x => x.IsWithinRange).Returns(false);

            closestShot = new Mock<IShotResult>();
            closestShot.Setup(x => x.DistanceToTarget).Returns(0);
            closestShot.Setup(x => x.IsWithinRange).Returns(true);

            secondClosestShot = new Mock<IShotResult>();
            secondClosestShot.Setup(x => x.DistanceToTarget).Returns(1);
            secondClosestShot.Setup(x => x.IsWithinRange).Returns(true);

            thirdClosestShot = new Mock<IShotResult>();
            thirdClosestShot.Setup(x => x.DistanceToTarget).Returns(2);
            thirdClosestShot.Setup(x => x.IsWithinRange).Returns(true);

        }

        [TestMethod]
        public void adding_single_out_range_shot_adds_one_to_shots()
        {
            var results = new ShotResultList(5);

            results.AddCandidateShot(outOfRangeShot.Object);

            var shots = results.Shots;

            Assert.AreEqual(1, shots.Count);
            Assert.AreSame(shots[0], outOfRangeShot.Object);
        }

        [TestMethod]
        public void adding_closer_out_of_range_shot_replaces_existing()
        {
            var results = new ShotResultList(5);

            results.AddCandidateShot(furtherOutOfRangeShot.Object);
            results.AddCandidateShot(outOfRangeShot.Object);

            var shots = results.Shots;

            Assert.AreEqual(1, shots.Count);
            Assert.AreSame(shots[0], outOfRangeShot.Object);

        }

        [TestMethod]
        public void adding_further_out_of_range_shot_does_not_replace_existing()
        {
            var results = new ShotResultList(5);

            results.AddCandidateShot(outOfRangeShot.Object);
            results.AddCandidateShot(furtherOutOfRangeShot.Object);

            var shots = results.Shots;

            Assert.AreEqual(1, shots.Count);
            Assert.AreSame(shots[0], outOfRangeShot.Object);

        }

        [TestMethod]
        public void adding_in_range_shot_replaces_out_of_range_shot()
        {
            var results = new ShotResultList(5);

            results.AddCandidateShot(outOfRangeShot.Object);
            results.AddCandidateShot(closestShot.Object);

            var shots = results.Shots;

            Assert.AreEqual(1, shots.Count);
            Assert.AreSame(shots[0], closestShot.Object);            
        }

        [TestMethod]
        public void closer_shot_moves_to_top()
        {
            var results = new ShotResultList(5);

            results.AddCandidateShot(secondClosestShot.Object);
            results.AddCandidateShot(closestShot.Object);

            var shots = results.Shots;

            Assert.AreEqual(2, shots.Count);
            Assert.AreSame(shots[0], closestShot.Object);
            Assert.AreSame(shots[1], secondClosestShot.Object);
        }

        [TestMethod]
        public void cannot_add_out_of_range_shot_when_in_range_exists()
        {
            var results = new ShotResultList(5);

            results.AddCandidateShot(closestShot.Object);
            results.AddCandidateShot(outOfRangeShot.Object);

            var shots = results.Shots;

            Assert.AreEqual(1, shots.Count);
            Assert.AreSame(shots[0], closestShot.Object);
        }

        [TestMethod]
        public void list_truncates_when_shot_limit_reached()
        {
            var results = new ShotResultList(2);

            results.AddCandidateShot(closestShot.Object);
            results.AddCandidateShot(thirdClosestShot.Object);
            results.AddCandidateShot(secondClosestShot.Object);

            var shots = results.Shots;

            Assert.AreEqual(2, shots.Count);
            Assert.AreSame(shots[0], closestShot.Object);
            Assert.AreEqual(shots[1], secondClosestShot.Object);
        }
    }
}
