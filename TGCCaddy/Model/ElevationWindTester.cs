using System;

namespace TGCCaddy.Model
{
    public class ElevationWindTester : IElevationWindTester
    {

        public ElevationWindTester(IWindAdjusterFactory windAdjusterFactory, IElevationAdjuster elevationAdjuster)
        {
            WindAdjusterFactory = windAdjusterFactory;
            ElevationAdjuster = elevationAdjuster;
        }

        /// <summary>
        /// Gets or sets the club
        /// </summary>
        public IClub Club { get; set; }

        /// <summary>
        /// Gets or sets the step
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// Gets or sets the elecvation
        /// </summary>
        public int Elevation
        {
            get { return this.ElevationAdjuster.Elevation; }
            set { this.ElevationAdjuster.Elevation = value; }
        }

        /// <summary>
        /// Windspeed
        /// </summary>
        public int WindSpeed
        {
            get { return this.WindAdjusterFactory.WindSpeed; }
            set { this.WindAdjusterFactory.WindSpeed = value; }
        }

        /// <summary>
        /// Direction
        /// </summary>
        public int WindDirection
        {
            get { return this.WindAdjusterFactory.WindDirection; }
            set { this.WindAdjusterFactory.WindSpeed = value; }
        }

        /// <summary>
        /// Gets or sets the shot type
        /// </summary>
        public ShotType ShotType { get; set; }

        /// <summary>
        /// Gets or sets the wind adjuster factory
        /// </summary>
        public IWindAdjusterFactory WindAdjusterFactory { get; set; }

        /// <summary>
        /// Gets or sets the elevation adjuster
        /// </summary>
        public IElevationAdjuster ElevationAdjuster { get; set; }

        public IElevationWindResult GetElevationResult(int carryDistance)
        {
            var clubDistance = this.Club.GetDistance(this.ShotType, this.Step);

            var elevationAdjustment = this.ElevationAdjuster.GetAdjustedDistance();

            //how far the shot should go without any wind effects
            var clubElevationDistance = clubDistance + (elevationAdjustment*-1);

            var windAdjuster = this.WindAdjusterFactory.GetWindAdjuster(this.ShotType);
            windAdjuster.AdjustForElevation = false;

            //how much the wind would affect the shot if elevation were not a factor
            var windDistance = windAdjuster.GetWindDistance(clubElevationDistance);
            var clubElevationWindDistance = clubElevationDistance + windDistance;
            var actualWindDistance = carryDistance - clubElevationDistance;


            var difference = actualWindDistance - windDistance;
            var percentDifference = difference != 0 ? Math.Round((double) difference/(double) windDistance, 2) : 0;
            var result = new ElevationWindResult()
            {
                ClubName = this.Club.Name,
                Step = this.Step,
                WindSpeed = this.WindSpeed,
                WindDirection = this.WindDirection,
                ExpectedDistance = clubElevationWindDistance,
                ActualDistance = carryDistance,
                ExpectedWindDistance = windDistance,
                ActualWindDistance = actualWindDistance,
                PercentageDistance = percentDifference
            };

            return result;
        }
        
    }
}