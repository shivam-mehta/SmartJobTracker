namespace SmartJobTracker.API.DTOs
{
    /// <summary>
    /// DTO returned to client for all job responses
    /// Includes DaysOld (calculated) and MatchScore for dashboard display
    /// </summary>
    public class JobResponseDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string JobUrl { get; set; } = string.Empty;
        public string JobDescription { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        // AI-generated match score 0-100
        public int MatchScore { get; set; }

        // Color theme helper for dashboard
        // <60=red, 60-70=yellow, 70-80=green, 80-90=blue, 90+=gold
        public string MatchScoreColor => MatchScore switch
        {
            >= 90 => "gold",
            >= 80 => "blue",
            >= 70 => "green",
            >= 60 => "yellow",
            _ => "red"
        };

        public DateTime DateFound { get; set; }
        public DateTime? DateApplied { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Calculated from DateFound - not stored in DB
        public int DaysOld { get; set; }

        // Days old warning for dashboard
        // green=fresh, yellow=7+ days, red=14+ days
        public string DaysOldWarning => DaysOld switch
        {
            >= 14 => "red",
            >= 7 => "yellow",
            _ => "green"
        };
    }
}