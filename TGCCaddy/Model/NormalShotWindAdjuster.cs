using System;
using System.Collections.Generic;

namespace TGCCaddy.Model
{
    public class NormalShotWindAdjuster : IWindAdjuster
    {
        #region Static lookups

        private static readonly int[,] distanceRanges =
        {
            {0, 75},
            {76, 95},
            {96, 120},
            {121, 132},
            {133, 145},
            {146, 158},
            {158, 170},
            {171, 181},
            {182, 195},
            {196, 207},
            {207, 500}
        };

        private static readonly IList<double[]> windPecentages = new List<double[]>
        {
            new[] {0.5, 0.4, 0.25, -0.1, -0.5, -0.75, -1},
            new[] {0.7, 0.6, 0.25, -0.2, -0.6, -1.2, -1.4},
            new[] {1.0, 0.9, 0.5, -0.2, -1.0, -1.6, -1.75},
            new[] {1.11, 1.0, 0.4, -0.2, -0.75, -1.4, -1.8},
            new[] {1.11, 1.0, 0.4, -0.2, -1.0, -1.6, -1.8},
            new[] {1.11, 1.0, 0.6, -0.2, -1.0, -1.6, -1.9},
            new[] {1.0, 0.9, 0.6, -0.2, -1.2, -1.5, -1.8},
            new[] {1.0, 0.9, 0.6, -0.2, -0.9, -1.3, -1.6},
            new[] {1.0, 0.7, 0.25, -0.2, -1.2, -1.5, -1.75},
            new[] {0.9, 0.7, 0.4, -0.2, -0.9, -1.5, -1.75},
            new[] {1.0, 0.75, 0.4, -0.2, -0.9, -1.5, -1.75}
        };

        #endregion

        private IList<WindPercentages> windRangePercentages;

        #region Helper classes

        private class WindPercentages
        {
            public int LowerValue { get; set; }
            public int UpperValue { get; set; }

            public double[] Percentages { get; private set; }

            public void AddPercentages(IEnumerable<double> percentages)
            {
                this.Percentages = new List<double>(percentages).ToArray();
            }
        }

        #endregion

        public NormalShotWindAdjuster(int elevation, int windSpeed, int windDirection)
        {
            this.Elevation = elevation;
            this.WindSpeed = windSpeed;
            this.WindDirection = windDirection;
            this.AdjustForElevation = true;
            CreatePercentages();
        }


        /// <summary>
        /// Get Elevation 
        /// </summary>
        public int Elevation { get; set; }

        /// <summary>
        /// Get Windspeed
        /// </summary>
        public int WindSpeed { get; set; }

        /// <summary>
        /// Get Wind Direction
        /// </summary>
        public int WindDirection { get; set; }

        /// <summary>
        /// Determines whether the wind is affected by elevation
        /// </summary>
        public bool AdjustForElevation { get; set; }

        public int GetWindAdjustedDistance(int distance)
        {
            return distance + GetWindDistance(distance);
        }

        /// <summary>
        /// Gets the adjusted wind distance
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public int GetWindDistance(int distance)
        {
            var clockIndex = GetClockIndex();

            foreach (var windPecentage in windRangePercentages)
            {
                if (distance >= windPecentage.LowerValue &&
                    distance <= windPecentage.UpperValue)
                {
                    var multiplier = windPecentage.Percentages[clockIndex];
                    var windEffect = (int)Math.Round(this.WindSpeed*multiplier,0);
                    var elevatedWindEffect = GetElevationAdjustement(windEffect);
                    return elevatedWindEffect;
                }
            }

            return 0;
        }

        /// <summary>
        /// Gets the adjustment for elevation
        /// </summary>
        /// <param name="windEffect"></param>
        /// <returns></returns>
        private int GetElevationAdjustement(int windEffect)
        {
            if (AdjustForElevation)
            {
                if (this.Elevation < 0 && windEffect > 0)
                {
                    return (int)Math.Round(windEffect * 1.2, 0);
                }
            }
            return windEffect;
        }

        private void CreatePercentages()
        {
            this.windRangePercentages = new List<WindPercentages>();

            const int lowerIndex = 0;
            const int upperIndex = 1;
            var count = distanceRanges.GetLength(0);

            for (int i = 0; i < count; i++)
            {

                var percentage = new WindPercentages()
                {
                    LowerValue = distanceRanges[i, lowerIndex],
                    UpperValue = distanceRanges[i, upperIndex],
                };

                percentage.AddPercentages(windPecentages[i]);

                this.windRangePercentages.Add(percentage);
            }
        }

        private int GetClockIndex()
        {
            switch (this.WindDirection)
            {
                case 0:
                    return 0;
                case 5:
                case 55:
                    return 1;
                case 10:
                case 50:
                    return 2;
                case 15:
                case 45:
                    return 3;
                case 20:
                case 40:
                    return 4;
                case 25:
                case 35:
                    return 5;
                case 30:
                    return 6;
                default:
                    return 3;

            }
        }
    }
}