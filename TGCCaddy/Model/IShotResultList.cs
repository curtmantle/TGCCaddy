using System.Collections.Generic;

namespace TGCCaddy.Model
{
    public interface IShotResultList
    {
        void AddCandidateShotRange(IEnumerable<IShotResult> candidateShots);
        void AddCandidateShot(IShotResult shot);
        IList<IShotResult> Shots { get; }
    }
}