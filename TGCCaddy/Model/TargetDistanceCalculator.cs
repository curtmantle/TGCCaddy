using System;

namespace TGCCaddy.Model
{
    /// <summary>
    /// Calculates the target distance
    /// </summary>
    public class TargetDistanceCalculator : ITargetDistanceCalculator
    {
        private readonly IElevationAdjuster elevationAdjuster;

        public TargetDistanceCalculator(IElevationAdjuster elevationAdjuster)
        {
            this.elevationAdjuster = elevationAdjuster;
        }

        /// <summary>
        /// Gets the elevation of the shot
        /// </summary>
        public int Elevation
        {
            get { return this.elevationAdjuster.Elevation; }
            set { this.elevationAdjuster.Elevation = value; }
        }

        /// <summary>
        /// Gets the distance of the shot
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// Gets or sets the expected roll
        /// </summary>
        public int Roll { get; set; }

        /// <summary>
        /// The percentage of full lie
        /// </summary>
        public double LiePercent { get; set; }

        /// <summary>
        /// Gets the target distance
        /// </summary>
        /// <returns></returns>
        public int GetTargetDistance()
        {
            var distanceWithRoll = this.Distance - this.Roll;
            var distanceLost = distanceWithRoll - (int)Math.Round(distanceWithRoll*LiePercent,0);

            var distanceAdjustedForLie = distanceWithRoll + distanceLost;
            return distanceAdjustedForLie + elevationAdjuster.GetAdjustedDistance();
        }
    }
}