using System.ComponentModel.DataAnnotations;

namespace SmartJobTracker.API.DTOs
{
    /// <summary>
    /// DTO for quick job analysis - no DB save required
    /// ResumeText is optional - auto-loads default resume if not provided
    /// User decides to save to tracker or discard after seeing match score
    /// </summary>
    public class QuickAnalyzeDto
    {
        /// <summary>
        /// The job description to analyze - pasted directly from job posting
        /// </summary>
        [Required(ErrorMessage = "Job description is required")]
        public string JobDescription { get; set; } = string.Empty;

        /// <summary>
        /// Optional - if not provided, default resume is auto-loaded
        /// </summary>
        public string? ResumeText { get; set; }
    }
}