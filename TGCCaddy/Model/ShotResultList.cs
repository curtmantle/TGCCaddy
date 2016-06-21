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
            //if the shot is within range, remove all shots outside range
            if (shot.IsWithinRange)
            {
                var outOfRange = this.Shots.Where(x => x.IsWithinRange == false);
                this.shots = this.Shots.Except(outOfRange).ToList();
            }
            var count = this.shots.Count;
            int insertPos = count;

            //now insert shot 
            if (count > 0)
            {

                for (int i = 0; i < shots.Count; i++)
                {
                    var shotResult = shots[i];
                    if (shot.DistanceToTarget < shotResult.DistanceToTarget)
                    {
                        insertPos = i;
                    }
                    else if (shot.DistanceToTarget == shotResult.DistanceToTarget)
                    {
                        insertPos = i + 1;
                    }

                }
            }

            if (insertPos == count)
            {
                this.shots.Add(shot);
            }
            else
            {
                this.Shots.Insert(insertPos, shot);
            }

            this.shots = shots.Take(this.maximumShots).ToList();
        }

        private void CheckOutOfRangeShot(IShotResult shot)
        {
            if (!shot.IsWithinRange)
            {
                if (shots.Count > 0)
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
                else
                {
                    this.shots.Add(shot);
                }
            }
        }

        public IList<IShotResult> Shots
        {
            get { return this.shots; }
        }

        

    }
}