namespace TGCCaddy.Model
{
    public interface IElevationWindResult
    {
        int ExpectedWindDistance { get; set; }

        int ActualWindDistance { get; set; }

        int Difference { get; }

        double PercentageDistance { get; set; }
        string ClubName { get; set; }
        int Step { get; set; }
        int WindDirection { get; set; }
        int WindSpeed { get; set; }
        int ExpectedDistance { get; set; }
        int ActualDistance { get; set; }
    }
}