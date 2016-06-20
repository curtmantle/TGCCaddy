using System.Collections.Generic;
using System.Linq;

namespace TGCCaddy.Model
{
    /// <summary>
    /// Maintains a list of shot results up to the maximum number
    /// </summary>
    public class ShotResultList : IShotResultList
    {
        private IList<IShotResult> shots;
        private readonly int maximumShots;

        /// <summary>
        /// Initializer for ShotResultList
        /// </summary>
        /// <param name="maximumShots"></param>
        public ShotResultList(int maximumShots)
        {
            this.maximumShots = maximumShots;
            this.shots = new List<IShotResult>();
        }

        /// <summary>
        /// Adds  shots from a list of candidates
        /// </summary>
        /// <param name="candidateShots"></param>
        public void AddCandidateShotRange(IEnumerable<IShotResult> candidateShots)
        {
            foreach (var candidateShot in candidateShots)
            {
                AddCandidateShot(candidateShot);    
            }
        }

        /// <summary>
        /// Adds shots from another ShotList
        /// </summary>
        /// <param name="list"></param>
        public void AddCandidateShotRange(IShotResultList list)
        {
            this.AddCandidateShotRange(list.Shots);
        }

        /// <summary>
        /// Adds a single candidate shot
        /// </summary>
        /// <param name="shot"></param>
        public void AddCandidateShot(IShotResult shot)
        {
            
            if (!shot.IsWithinRange)
            {
                CheckOutOfRangeShot(shot);
            }
            else
            {
                CheckInRangeShot(shot);
            }

        }

        private void CheckInRangeShot(IShotResult shot)
        {
            var shotList = new List<IShotResult>();

            foreach (var shotResult in shots)
            {
                if (shot.IsWithinRange && !shotResult.IsWithinRange)
                {
                    shotList.Add(shot);
                }
                else if (shot.IsWithinRange && shot.DistanceToTarget < shotResult.DistanceToTarget)
                {
                    shotList.Add(shot);
                    shotList.Add(shotResult);
                }
                else if (shot.DistanceToTarget == shotResult.DistanceToTarget)
                {
                    shotList.Add(shotResult);
                    shotList.Add(shot);
                }
                else
                {
                    shotList.Add(shotResult);
                }
            }
            this.shots = shotList.Take(this.maximumShots).ToList();
        }

        private void CheckOutOfRangeShot(IShotResult shot)
        {
            if (!shot.IsWithinRange)
            {
                foreach (var shotResult in Shots)
                {
                    if (!shotResult.IsWithinRange)
                    {
                        if (shot.DistanceToTarget < shotResult.DistanceToTarget)
                        {
                            this.shots = new List<IShotResult>() { shot };
                            return;
                        }
                        else if (shot.DistanceToTarget == shotResult.DistanceToTarget)
                        {
                            this.Shots.Add(shot);
                            return;
                        }
                    }
                }
            }
        }

        public IList<IShotResult> Shots
        {
            get { return this.shots; }
        }

        

    }
}