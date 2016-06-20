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
        /// Calculates the target distance
        /// </summary>
        private ITargetDistanceCalculator targetDistanceCalculator;

        /// <summary>
        /// Factory providing the wind adjuster for a shot type
        /// </summary>
        private IWindAdjusterFactory windAdjusterFactory;

        /// <summary>
        /// List of the clubs
        /// </summary>
        private IList<IClub> clubs;

        public ShotCalculator(IList<IClub> clubs,
                              ITargetDistanceCalculator targetDistanceCalculator, 
                              IWindAdjusterFactory windAdjusterFactory)
        {
            this.clubs = clubs;
            this.targetDistanceCalculator = targetDistanceCalculator;
            this.windAdjusterFactory = windAdjusterFactory;
        }

        /// <summary>
        /// Gets the distance to the target
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// Gets the elevation of the target
        /// </summary>
        public int Elevation { get; set; }

        /// <summary>
        /// gets the expected roll
        /// </summary>
        public int Roll { get; set; }

        /// <summary>
        /// Gets the wind speed
        /// </summary>
        public int WindSpeed { get; set; }

        /// <summary>
        /// Gets or sets the direction of the wind
        /// </summary>
        public int WindDirection { get; set; }

        /// <summary>
        /// Gets or sets the maximum distance to target of results
        /// </summary>
        public int MaximumDistance { get; set; }

        /// <summary>
        /// gets the target distance
        /// </summary>
        public int TargetDistance { get; private set; }

        /// <summary>
        /// Calculates the matches for the shot
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IShotResult> Calculate()
        {
            InitializeWindFactory();
            
            var targetDistance = GetTargetDistance();

            var shotList = new ShotResultList(10);

            foreach (var club in this.clubs)
            {
                var clubShots = GetShotsForClub(club, targetDistance);

                shotList.AddCandidateShotRange(clubShots);
            }

            return shotList.Shots;
        }

        private void InitializeWindFactory()
        {
            this.windAdjusterFactory.Elevation = this.Elevation;
            this.windAdjusterFactory.WindSpeed = this.WindSpeed;
            this.windAdjusterFactory.WindDirection = this.WindDirection;
        }


        /// <summary>
        /// Gets the target distance of the shot based on elevation and roll
        /// </summary>
        private int GetTargetDistance()
        {
            targetDistanceCalculator.Elevation = this.Elevation;
            targetDistanceCalculator.Distance = this.Distance;
            targetDistanceCalculator.Roll = this.Roll;

            return targetDistanceCalculator.GetTargetDistance();
            
        }

        private IEnumerable<IShotResult> GetShotsForClub(IClub club, int targetDistance)
        {
            var shotList = new ShotResultList(10);

            var shotTypes = Enum.GetValues(typeof(ShotType)).Cast<ShotType>();
            foreach (var shotType in shotTypes)
            {
                var windAdjuster = this.windAdjusterFactory.GetWindAdjuster(shotType);

                var stepCount = club.GetStepCountForShotType(shotType);

                for (int i = 0; i < stepCount; i++)
                {
                    var methods = new Func<ShotType, int, int>[] {club.GetDistance, club.GetHalfDistance };

                    foreach (var method in methods)
                    {
                        var distance = method(shotType, i);

                        if (distance > 0)
                        {
                            var windAdjustedDistance = windAdjuster.GetWindAdjustedDistance(distance);

                            var result = new ShotResult()
                            {
                                ClubName = club.Name,
                                Step = i,
                                ShotType = shotType,
                                Distance =  distance,
                                WindDistance = windAdjustedDistance,
                                DistanceToTarget = targetDistance - windAdjustedDistance
                            };
                            shotList.AddCandidateShot(result);
                        }                        
                    }
                }
            }

            return shotList.Shots;
        }

    }
}
