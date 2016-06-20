using System.Collections.Generic;

namespace TGCCaddy.Model
{
    public interface IClub
    {
        string Name { get; }
        void AddShotDistances(ShotType shotType, IEnumerable<int> distances);
        int GetDistance(ShotType shotType, int step);
        int GetHalfDistance(ShotType shotType, int stepBefore);

        /// <summary>
        /// Gets the number of steps for a particular shot type for this club
        /// </summary>
        int GetStepCountForShotType(ShotType shotType);
    }
}