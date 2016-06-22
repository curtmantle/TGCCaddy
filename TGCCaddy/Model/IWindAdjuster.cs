namespace TGCCaddy.Model
{
    public interface IWindAdjuster
    {
        int GetWindAdjustedDistance(int distance);
    }

    public class NullShotWindAdjuster : IWindAdjuster
    {
        public int GetWindAdjustedDistance(int distance)
        {
            return distance;
        }
    }

}