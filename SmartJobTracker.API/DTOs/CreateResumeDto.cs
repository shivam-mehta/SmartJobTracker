using System.ComponentModel.DataAnnotations;
using SmartJobTracker.API.Models;

namespace SmartJobTracker.API.DTOs
{
    /// <summary>
    /// DTO for creating a new resume version
    /// Supports multiple tailored versions for different job types
    /// </summary>
    public class CreateResumeDto
    {
        // Friendly name e.g. "Senior .NET Developer", "Solutions Engineer"
        [Required(ErrorMessage = "Version name is required")]
        [MaxLength(200, ErrorMessage = "Version name cannot exceed 200 characters")]
        public string VersionName { get; set; } = string.Empty;

        // Professional summary - tailored per version
        [Required(ErrorMessage = "Summary is required")]
        public string Summary { get; set; } = string.Empty;

        // List of skills - AI uses these for gap analysis
        [Required(ErrorMessage = "At least one skill is required")]
        public List<string> Skills { get; set; } = new();

        // Work experience entries - AI uses these for matching
        public List<WorkExperience> Experience { get; set; } = new();

        // Certifications list
        public List<string> Certifications { get; set; } = new();

        // Full resume text - for AI analysis
        // Can be auto-generated from above fields or manually provided
        public string FullResumeText { get; set; } = string.Empty;

        // Set as default resume for quick analyze?
        public bool IsDefault { get; set; } = false;
    }
}