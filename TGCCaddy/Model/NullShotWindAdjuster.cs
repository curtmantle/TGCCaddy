namespace TGCCaddy.Model
{
    public class NullShotWindAdjuster : IWindAdjuster
    {
        public int GetWindDistance(int distance)
        {
            return 0;
        }

        public int GetWindAdjustedDistance(int distance)
        {
            return distance;
        }

        public bool AdjustForElevation { get; set; }
    }
}