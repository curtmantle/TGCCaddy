using System.Collections.Generic;

namespace TGCCaddy.Model
{
    public interface IShotCalculator
    {
        /// <summary>
        /// Gets or sets the maximum distance to target of results
        /// </summary>
        int MaximumDistance { get; set; }

        /// <summary>
        /// Calculates the matches for the shot
        /// </summary>
        /// <returns></returns>
        IEnumerable<IShotResult> Calculate(int targetDistance);
    }
}