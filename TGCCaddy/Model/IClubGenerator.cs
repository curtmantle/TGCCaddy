using System.Collections.Generic;

namespace TGCCaddy.Model
{
    public interface IClubGenerator
    {
        IList<IClub> GetClubs();
    }
}