namespace TGCCaddy.Model
{
    public class WindAdjusterFactory : IWindAdjusterFactory
    {
        public int Elevation { get; set; }
        public int WindSpeed { get; set; }
        public int WindDirection { get; set; }

        public IWindAdjuster GetWindAdjuster(ShotType shotType)
        {
            switch (shotType)
            {
                case ShotType.Normal:
                    return new NormalShotWindAdjuster(this.Elevation, this.WindSpeed, this.WindDirection);
                default:
                    return new NullShotWindAdjuster();
            }
        }
    }
}