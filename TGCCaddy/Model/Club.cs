using System.Collections.Generic;

namespace TGCCaddy.Model
{
    /// <summary>
    /// Represents a club and the distances it can hit for each shot
    /// </summary>
    public class Club : IClub
    {
        private Dictionary<ShotType, IList<int>> shotDistances;

        public Club(string name)
        {
            this.Name = name;
            this.shotDistances = new Dictionary<ShotType, IList<int>>();
        }

        /// <summary>
        /// Gets the name of the club
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Adds a set of distances to the club for a specific shot type
        /// </summary>
        public void AddShotDistances(ShotType shotType, IEnumerable<int> distances)
        {
            var newDistances = new List<int>(distances);
            if (!this.shotDistances.ContainsKey(shotType))
            {
                this.shotDistances.Add(shotType, newDistances);
            }
            else
            {
                this.shotDistances[shotType] = newDistances;
            }
        }

        /// <summary>
        /// Gets the number of steps for a particular shot type for this club
        /// </summary>
        public int GetStepCountForShotType(ShotType shotType)
        {
            if (shotDistances.ContainsKey(shotType))
            {
                return shotDistances[shotType].Count;
            }
            return 0;
        }

        /// <summary>
        /// Gets a distance based for a specific shot type at a specific step
        /// </summary>
        public int GetDistance(ShotType shotType, int step)
        {
            if (shotDistances.ContainsKey(shotType))
            {
                var steps = shotDistances[shotType];

                if (step >= 0 && step < steps.Count)
                {
                    return steps[step];
                }
            }
            return 0;
        }
        
        /// <summary>
        /// Gets a distance for a specific shot type for a half step
        /// </summary>
        public int GetHalfDistance(ShotType shotType, int stepBefore)
        {
            if (shotDistances.ContainsKey(shotType))
            {
                var steps = shotDistances[shotType];

                if (stepBefore >= 0 && stepBefore < (steps.Count - 1))
                {
                    return steps[stepBefore] + ((steps[stepBefore + 1] - steps[stepBefore])/2);
                }
            }
            return 0;
        }
    }
}