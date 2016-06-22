namespace TGCCaddy.Model
{
    public interface ITargetDistanceCalculator
    {
        int GetTargetDistance();
        int Distance { get; set; }
        int Roll { get; set; }
        int Elevation { get; set; }
        double LiePercent { get; set; }
    }
}