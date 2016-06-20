using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGCCaddy.Model
{
    public class ClubGenerator : IClubGenerator
    {
        private bool generated = false;

        private IClub[] clubs =
        {
            new Club("Lob Wedge"),
            new Club("Sand Wedge"),
            new Club("Pitching Wedge"),
            new Club("9 Iron"),
            new Club("8 Iron"),
            new Club("7 Iron"),
            new Club("6 Iron"),
            new Club("5 Iron"),
            new Club("4 Iron"),
            new Club("3 Iron"),
            new Club("5 Wood"),
            new Club("3 Wood"),
            new Club("Driver")
        };

        private IList<int[]> normalShots = new List<int[]>()
        {
            new[] {75, 65},   //LW
            new[] {95, 80},   //SW
            new[] {120, 100}, //PW
            new[] {132, 116}, //9i
            new[] {145, 126}, //8i
            new[] {158, 139}, //7i
            new[] {170, 153}, //6i
            new[] {181, 167}, //5i
            new[] {195, 183}, //4i
            new[] {207, 198}, //3i
            new[] {225, 225}, //5W
            new[] {245, 245}, //3W
            new[] {279, 279}  //DR
        };


        public IList<IClub> GetClubs()
        {
            if (!this.generated)
            {
                int count = this.clubs.Length;

                for (int index = 0; index < count; index++)
                {
                    var club = this.clubs[index];
                    club.AddShotDistances(ShotType.Normal, normalShots[index]);
                }

                this.generated = true;
            }
            return this.clubs;
        }



    }
}
