namespace TGCCaddy.Model
{
    public interface IWindAdjusterFactory
    {
        /// <summary>
        /// The Elevation of the shot
        /// </summary>
        int Elevation { get; set; }

        /// <summary>
        /// The Windspeed
        /// </summary>
        int WindSpeed { get; set; }

        /// <summary>
        /// The wind direction
        /// </summary>
        int WindDirection { get; set; }

        IWindAdjuster GetWindAdjuster(ShotType shotType);
    }
}