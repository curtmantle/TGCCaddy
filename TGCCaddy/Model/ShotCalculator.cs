using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Editors.Internal;

namespace TGCCaddy.Model
{
    public enum ShotType
    {
        Normal, 
        Pitch,
        Chip,
        Flop
    }

    /// <summary>
    /// Calculates the shots
    /// </summary>
    public class ShotCalculator : IShotCalculator
    {

        /// <summary>
        /// Factory providing the wind adjuster for a shot type
        /// </summary>
        private readonly IWindAdjusterFactory windAdjusterFactory;

        /// <summary>
        /// List of the clubs
        /// </summary>
        private readonly IList<IClub> clubs;

        public ShotCalculator(IList<IClub> clubs,
                              IWindAdjusterFactory windAdjusterFactory)
        {
            this.clubs = clubs;
            this.windAdjusterFactory = windAdjusterFactory;
        }

        /// <summary>
        /// Gets or sets the maximum distance to target of results
        /// </summary>
        public int MaximumDistance { get; set; }

        /// <summary>
        /// Calculates the matches for the shot
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IShotResult> Calculate(int targetDistance)
        {

            var shotList = new ShotResultList(10);

            foreach (var club in this.clubs)
            {
                var clubShots = GetShotsForClub(club, targetDistance);

                shotList.AddCandidateShotRange(clubShots);
            }

            return shotList.Shots;
        }


        private IEnumerable<IShotResult> GetShotsForClub(IClub club, int targetDistance)
        {
            var clubShotList = new ShotResultList(10);

            var shotTypes = Enum.GetValues(typeof(ShotType)).Cast<ShotType>();
            foreach (var shotType in shotTypes)
            {
                var windAdjuster = this.windAdjusterFactory.GetWindAdjuster(shotType);

                var stepCount = club.GetStepCountForShotType(shotType);

                for (var i = 0; i < stepCount; i++)
                {
                    var methods = new Func<ShotType, int, int>[] {club.GetDistance, club.GetHalfDistance };

                    for (var index = 0; index < methods.Length; index++)
                    {
                        var method = methods[index];
                        var distance = method(shotType, i);

                        var step = index == 1 ? i + 0.5d : i;
                        if (distance > 0)
                        {
                            var windAdjustedDistance = windAdjuster.GetWindAdjustedDistance(distance);
                            var distanceToTarget = Math.Abs(windAdjustedDistance - targetDistance);
                            var result = new ShotResult()
                            {
                                ClubName = club.Name,
                                Step = step,
                                ShotType = shotType,
                                Distance = distance,
                                WindDistance = windAdjustedDistance,
                                DistanceToTarget = distanceToTarget,
                                IsWithinRange = distanceToTarget <= MaximumDistance
                            };
                            clubShotList.AddCandidateShot(result);
                        }
                    }
                }
            }

            return clubShotList.Shots;
        }

    }
}
