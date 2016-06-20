namespace TGCCaddy.Model
{
    public class ShotResult : IShotResult
    {
        /// <summary>
        /// Gets the name of the club
        /// </summary>
        public string ClubName { get; set; }

        /// <summary>
        /// Gets the loft step
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// Gets the shot type
        /// </summary>
        public ShotType ShotType { get; set; }
        
        /// <summary>
        /// Gets the distance of the shot
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// Gets the distance of the shot after wind has been factored in
        /// </summary>
        public int WindDistance { get; set; }

        /// <summary>
        /// Gets the distance the shot will be from the target
        /// </summary>
        public int DistanceToTarget { get; set; }

        /// <summary>
        /// Gets or sets whether a shot is within the required range
        /// </summary>
        public bool IsWithinRange { get; set; }
    }
}