namespace TGCCaddy.Model
{
    public interface IElevationWindTester
    {
        /// <summary>
        /// Gets or sets the club
        /// </summary>
        IClub Club { get; set; }

        /// <summary>
        /// Gets or sets the step
        /// </summary>
        int Step { get; set; }

        int Elevation { get; set; }

        /// <summary>
        /// Gets or sets the shot type
        /// </summary>
        ShotType ShotType { get; set; }

        /// <summary>
        /// Gets or sets the wind adjuster factory
        /// </summary>
        IWindAdjusterFactory WindAdjusterFactory { get; set; }

        /// <summary>
        /// Gets or sets the elevation adjuster
        /// </summary>
        IElevationAdjuster ElevationAdjuster { get; set; }

        /// <summary>
        /// Windspeed
        /// </summary>
        int WindSpeed { get; set; }

        /// <summary>
        /// Direction
        /// </summary>
        int WindDirection { get; set; }

        IElevationWindResult GetElevationResult(int carryDistance);
    }
}