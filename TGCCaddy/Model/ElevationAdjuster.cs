using System;

namespace TGCCaddy.Model
{
    /// <summary>
    /// Standard elevation adjuster that takes an elevation and multiplies
    /// by a particular number.
    /// </summary>
    public class ElevationAdjuster : IElevationAdjuster
    {
        public ElevationAdjuster(double multiplier)
        {
            this.Multiplier = multiplier;
        }

        public ElevationAdjuster() : this(0.27)
        {

        }

        /// <summary>
        /// Gets or sets the elevation
        /// </summary>
        public int Elevation { get; set; }

        /// <summary>
        /// Gets or sets the multiplier used to calculate the elevation
        /// </summary>
        public double Multiplier { get; set; } 

        public int GetAdjustedDistance()
        {
            return (int)Math.Round(this.Elevation*this.Multiplier,0);
        }
    }
}