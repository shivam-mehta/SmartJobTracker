namespace SmartJobTracker.API.Models
{
    /// <summary>
    /// Core job entity - represents a job saved to the tracker
    /// MatchScore is AI-generated, DaysOld is calculated on the fly
    /// </summary>
    public class Job
    {
        public int Id { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string JobTitle { get; set; } = string.Empty;

        public string JobUrl { get; set; } = string.Empty;

        public string JobDescription { get; set; } = string.Empty;

        // New, Applied, Interview, Offer, Rejected
        public string Status { get; set; } = "New";

        // AI-generated match score 0-100
        public int MatchScore { get; set; } = 0;

        public DateTime DateFound { get; set; } = DateTime.UtcNow;

        public DateTime? DateApplied { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Calculated - not stored in DB
        // [NotMapped] tells EF Core to ignore this property
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int DaysOld => (DateTime.UtcNow - DateFound).Days;
    }
}