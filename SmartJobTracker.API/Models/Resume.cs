using System.Text.Json;

namespace SmartJobTracker.API.Models
{
    /// <summary>
    /// Flexible resume model - supports multiple tailored versions
    /// Skills and Experience stored as JSON for flexibility
    /// AI uses full ResumeText for analysis but structured fields
    /// allow future filtering and dashboard features
    /// </summary>
    public class Resume
    {
        public int Id { get; set; }

        // Friendly name e.g. "Senior .NET Developer", "Solutions Engineer"
        public string VersionName { get; set; } = string.Empty;

        // Professional summary - 3-5 sentences, tailored per version
        public string Summary { get; set; } = string.Empty;

        // Skills stored as JSON array in DB
        // e.g. ["C#", "SQL Server", "Azure", "Docker"]
        // [NotMapped] computed properties handle serialization
        public string SkillsJson { get; set; } = "[]";

        // Work experience stored as JSON array in DB
        // Each entry = one job position with full details
        public string ExperienceJson { get; set; } = "[]";

        // Certifications stored as JSON array
        // e.g. ["Tosca Automation Specialist", "Kentico Developer"]
        public string CertificationsJson { get; set; } = "[]";

        // Full resume text - concatenated version for AI analysis
        // AI reads this for gap analysis - rich context
        public string FullResumeText { get; set; } = string.Empty;

        // Is this the default resume for quick analyze?
        // Only one can be default at a time
        public bool IsDefault { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Computed properties - not stored in DB
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<string> Skills
        {
            get => JsonSerializer.Deserialize<List<string>>(SkillsJson) ?? new();
            set => SkillsJson = JsonSerializer.Serialize(value);
        }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<WorkExperience> Experience
        {
            get => JsonSerializer.Deserialize<List<WorkExperience>>(ExperienceJson) ?? new();
            set => ExperienceJson = JsonSerializer.Serialize(value);
        }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<string> Certifications
        {
            get => JsonSerializer.Deserialize<List<string>>(CertificationsJson) ?? new();
            set => CertificationsJson = JsonSerializer.Serialize(value);
        }
    }

    /// <summary>
    /// Represents one work experience entry
    /// Stored as JSON inside Resume.ExperienceJson
    /// </summary>
    public class WorkExperience
    {
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }  // null = current job
        public string Description { get; set; } = string.Empty;

        // Key achievements - AI uses these for matching
        public List<string> Achievements { get; set; } = new();

        // Technologies used in this role
        public List<string> TechnologiesUsed { get; set; } = new();

        // Is this the current position?
        public bool IsCurrent => EndDate == null;
    }
}