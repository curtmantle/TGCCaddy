using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGCCaddy.Model
{
    /// <summary>
    /// Interface for classes that determine how much the distance
    /// is affected by elevation
    /// </summary>
    public interface IElevationAdjuster
    {
        int Elevation { get; set; }

        int GetAdjustedDistance();
    }
}
