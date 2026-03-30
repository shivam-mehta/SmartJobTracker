using SmartJobTracker.API.Models;

namespace SmartJobTracker.API.DTOs
{
    /// <summary>
    /// DTO returned to client for resume responses
    /// Includes deserialized Skills, Experience and Certifications lists
    /// </summary>
    public class ResumeResponseDto
    {
        public int Id { get; set; }

        // Friendly name e.g. "Senior .NET Developer"
        public string VersionName { get; set; } = string.Empty;

        // Professional summary
        public string Summary { get; set; } = string.Empty;

        // Deserialized skills list
        public List<string> Skills { get; set; } = new();

        // Deserialized work experience list
        public List<WorkExperience> Experience { get; set; } = new();

        // Deserialized certifications list
        public List<string> Certifications { get; set; } = new();

        // Full resume text for AI analysis
        public string FullResumeText { get; set; } = string.Empty;

        // Is this the default resume for quick analyze?
        public bool IsDefault { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}