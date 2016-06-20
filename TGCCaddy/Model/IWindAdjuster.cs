namespace TGCCaddy.Model
{
    public interface IWindAdjuster
    {
        int GetWindAdjustedDistance(int distance);
    }

    public class NormalShotWindAdjuster : IWindAdjuster
    {
        public NormalShotWindAdjuster(int elevation, int windSpeed, int windDirection)
        {
            
        }

        public int GetWindAdjustedDistance(int distance)
        {
            return distance;
        }

        public int Elevation { get; set; }

        public int WindSpeed { get; set; }

        public int WindDirection { get; set; }
    }

    public class NullShotWindAdjuster : IWindAdjuster
    {
        public int GetWindAdjustedDistance(int distance)
        {
            return distance;
        }
    }

}