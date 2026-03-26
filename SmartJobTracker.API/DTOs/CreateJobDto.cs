using System.ComponentModel.DataAnnotations;

namespace SmartJobTracker.API.DTOs
{
    /// <summary>
    /// DTO for creating a new job in the tracker
    /// MatchScore is optional - can be set from AI analysis result
    /// </summary>
    public class CreateJobDto
    {
        [Required(ErrorMessage = "Company name is required")]
        [MaxLength(200, ErrorMessage = "Company name cannot exceed 200 characters")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Job title is required")]
        [MaxLength(200, ErrorMessage = "Job title cannot exceed 200 characters")]
        public string JobTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Job URL is required")]
        [Url(ErrorMessage = "Please provide a valid URL")]
        [MaxLength(500, ErrorMessage = "URL cannot exceed 500 characters")]
        public string JobUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Job description is required")]
        public string JobDescription { get; set; } = string.Empty;

        // Status defaults to New
        public string Status { get; set; } = "New";

        // AI match score - optional on create, defaults to 0
        // Gets set from quick analyze result before saving
        public int MatchScore { get; set; } = 0;

        public DateTime DateFound { get; set; } = DateTime.UtcNow;
    }
}