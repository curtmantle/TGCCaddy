namespace TGCCaddy.Model
{
    public class ElevationWindResult : IElevationWindResult
    {
        public string ClubName { get; set; }
        public int Step { get; set; }
        public int WindDirection { get; set; }
        public int WindSpeed { get; set; }
        public int ExpectedDistance { get; set; }
        public int ActualDistance { get; set; }
        public int ExpectedWindDistance { get; set; }
        public int ActualWindDistance { get; set; }
        public int Difference
        {
            get { return ActualWindDistance - ExpectedWindDistance; }
        }
        public double PercentageDistance { get; set; }
    }
}