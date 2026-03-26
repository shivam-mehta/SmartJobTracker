using System.ComponentModel.DataAnnotations;

namespace SmartJobTracker.API.DTOs
{
    /// <summary>
    /// DTO for updating an existing job in the tracker
    /// Includes MatchScore and DateApplied as these change over time
    /// </summary>
    public class UpdateJobDto
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

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("New|Applied|Interview|Offer|Rejected",
            ErrorMessage = "Status must be: New, Applied, Interview, Offer, or Rejected")]
        public string Status { get; set; } = "New";

        // AI match score - updated when re-analyzed
        public int MatchScore { get; set; } = 0;

        public DateTime DateFound { get; set; }

        public DateTime? DateApplied { get; set; }
    }
}