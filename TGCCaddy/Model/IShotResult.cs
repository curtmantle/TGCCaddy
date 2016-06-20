namespace TGCCaddy.Model
{
    public interface IShotResult
    {
        /// <summary>
        /// Gets the name of the club
        /// </summary>
        string ClubName { get; set; }

        /// <summary>
        /// Gets the loft step
        /// </summary>
        int Step { get; set; }

        /// <summary>
        /// Gets the shot type
        /// </summary>
        ShotType ShotType { get; set; }

        /// <summary>
        /// Gets the distance of the shot
        /// </summary>
        int Distance { get; set; }

        /// <summary>
        /// Gets the distance of the shot after wind has been factored in
        /// </summary>
        int WindDistance { get; set; }

        /// <summary>
        /// Gets the distance the shot will be from the target
        /// </summary>
        int DistanceToTarget { get; set; }

        /// <summary>
        /// Gets whether the shot is within range
        /// </summary>
        bool IsWithinRange { get; set; }
    }
}