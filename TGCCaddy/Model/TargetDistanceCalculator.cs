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
        /// Gets the target distance
        /// </summary>
        /// <returns></returns>
        public int GetTargetDistance()
        {
            return this.Distance - this.Roll + elevationAdjuster.GetAdjustedDistance();
        }
    }
}