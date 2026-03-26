using System.ComponentModel.DataAnnotations;

namespace SmartJobTracker.API.DTOs
{
    /// <summary>
    /// DTO for quick job analysis - no DB save required
    /// User pastes job description + resume text for instant AI comparison
    /// Save to tracker is optional after seeing the match score
    /// </summary>
    public class QuickAnalyzeDto
    {
        /// <summary>
        /// The job description to analyze - pasted directly from job posting
        /// </summary>
        [Required(ErrorMessage = "Job description is required")]
        public string JobDescription { get; set; } = string.Empty;

        /// <summary>
        /// The candidate's resume text - pasted or loaded from stored resume
        /// </summary>
        [Required(ErrorMessage = "Resume text is required")]
        public string ResumeText { get; set; } = string.Empty;
    }
}