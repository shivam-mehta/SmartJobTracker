namespace SmartJobTracker.API.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string JobUrl { get; set; } = string.Empty;
        public string JobDescription { get; set; } = string.Empty;
        public string Status { get; set; } = "New";
        public string? Notes { get; set; }
        public DateTime DateFound { get; set; } = DateTime.UtcNow;
        public DateTime? DateApplied { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}