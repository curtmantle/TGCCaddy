using System.Collections.Generic;

namespace TGCCaddy.Model
{
    public interface IShotCalculator
    {
        /// <summary>
        /// Gets the distance to the target
        /// </summary>
        int Distance { get; set; }

        /// <summary>
        /// Gets the elevation of the target
        /// </summary>
        int Elevation { get; set; }

        /// <summary>
        /// gets the expected roll
        /// </summary>
        int Roll { get; set; }

        /// <summary>
        /// Gets the wind speed
        /// </summary>
        int WindSpeed { get; set; }

        /// <summary>
        /// Gets or sets the direction of the wind
        /// </summary>
        int WindDirection { get; set; }

        /// <summary>
        /// Gets or sets the maximum distance to target of results
        /// </summary>
        int MaximumDistance { get; set; }

        /// <summary>
        /// gets the target distance
        /// </summary>
        int TargetDistance { get; }

        /// <summary>
        /// Calculates the matches for the shot
        /// </summary>
        /// <returns></returns>
        IEnumerable<IShotResult> Calculate();
    }
}