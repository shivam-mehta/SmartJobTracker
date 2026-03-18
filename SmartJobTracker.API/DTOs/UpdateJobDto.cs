using System.ComponentModel.DataAnnotations;

namespace SmartJobTracker.API.DTOs
{
    /// <summary>
    /// DTO used when UPDATING an existing job
    /// Similar to CreateJobDto but includes Status and DateApplied
    /// because those are fields that change as job progresses
    /// Notice: Still no Id, CreatedAt, UpdatedAt - server manages those
    /// </summary>
    public class UpdateJobDto
    {
        /// <summary>
        /// Company name - required, max 200 characters
        /// </summary>
        [Required(ErrorMessage = "Company name is required")]
        [MaxLength(200, ErrorMessage = "Company name cannot exceed 200 characters")]
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// Job title - required, max 200 characters
        /// </summary>
        [Required(ErrorMessage = "Job title is required")]
        [MaxLength(200, ErrorMessage = "Job title cannot exceed 200 characters")]
        public string JobTitle { get; set; } = string.Empty;

        /// <summary>
        /// Job URL - required, must be valid URL format
        /// </summary>
        [Required(ErrorMessage = "Job URL is required")]
        [Url(ErrorMessage = "Please provide a valid URL")]
        [MaxLength(500, ErrorMessage = "URL cannot exceed 500 characters")]
        public string JobUrl { get; set; } = string.Empty;

        /// <summary>
        /// Full job description - required for AI gap analysis
        /// </summary>
        [Required(ErrorMessage = "Job description is required")]
        public string JobDescription { get; set; } = string.Empty;

        /// <summary>
        /// Application status - tracks job progress
        /// Valid values: New, Applied, Interview, Offer, Rejected
        /// </summary>
        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("New|Applied|Interview|Offer|Rejected",
            ErrorMessage = "Status must be: New, Applied, Interview, Offer, or Rejected")]
        public string Status { get; set; } = "New";

        /// <summary>
        /// Optional notes - max 1000 characters
        /// </summary>
        [MaxLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        /// <summary>
        /// Date job was found
        /// </summary>
        public DateTime DateFound { get; set; }

        /// <summary>
        /// Date application was submitted - nullable because
        /// you may update other fields before actually applying
        /// </summary>
        public DateTime? DateApplied { get; set; }
    }
}