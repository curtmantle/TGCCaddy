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

        private readonly IList<int[]> normalShots = new List<int[]>()
        {
            new[] {75, 64, 54, 45, 39},   //LW
            new[] {95, 80, 67, 55, 47},   //SW
            new[] {120, 100, 82, 65, 54}, //PW
            new[] {132, 113, 93, 75, 64}, //9i
            new[] {145, 126, 106, 87, 75}, //8i
            new[] {158, 139, 120, 100, 87}, //7i
            new[] {170, 153, 134, 114, 101}, //6i
            new[] {181, 167, 149, 130, 116}, //5i
            new[] {195, 183, 166, 146, 131}, //4i
            new[] {207, 198, 182, 162, 147}, //3i
            new[] {225}, //5W
            new[] {245}, //3W
            new[] {279}  //DR
        };

        private readonly IList<int[]> pitchShots = new List<int[]>()
        {
            new[] {45,37,30,24,20},   //LW
            new[] {55,46,37,29,24},   //SW
            new[] {75,62,50,39,31}, //PW
            new[] {80,68,56,45,37}, //9i
            new[] {90,78,66,54,46}, //8i
            new[] {95,85,74,62,54}, //7i
            new[] {100,92,83,71,63}, //6i
        };

        private readonly IList<int[]> flopShots = new List<int[]>()
        {
            new [] {30, 24, 20}, //LW
            new [] {35, 30, 24 }, //SW
            new [] {40, 37, 30}
        };

        private readonly IList<int[]> chipShots = new List<int[]>()
        {
            new [] { 7, 7, 3, 2, 2, 1, 1 },
            new [] { 11, 10, 5, 4, 3, 2, 2 },
            new [] { 16, 14, 9, 6, 5, 3, 2 },
            new [] { 21, 17, 11, 9, 6, 4, 2 },
            new [] { 27, 25, 18, 13, 10, 8, 4 },
            new [] { 30, 27, 20, 15, 12, 10, 7, 6 },
            new [] { 33, 29, 22, 18, 15, 11, 9, 7 },
            new [] { 38, 34, 27, 22, 18, 18, 15, 12, 10}
        };

        public IList<IClub> GetClubs()
        {
            if (!this.generated)
            {
                int count = this.clubs.Length;

                for (int index = 0; index < count; index++)
                {
                    var club = this.clubs[index];
                    if (index < normalShots.Count)
                    {
                        club.AddShotDistances(ShotType.Normal, normalShots[index]);
                    }

                    if (index < pitchShots.Count)
                    {
                        club.AddShotDistances(ShotType.Pitch, pitchShots[index]);
                    }

                    if (index < flopShots.Count)
                    {
                        club.AddShotDistances(ShotType.Flop, flopShots[index]);
                    }

                    if (index < chipShots.Count)
                    {
                        club.AddShotDistances(ShotType.Chip, chipShots[index]);
                    }
                }

                this.generated = true;

            }
            return this.clubs;
        }



    }
}
