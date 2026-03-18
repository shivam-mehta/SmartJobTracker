using System.ComponentModel.DataAnnotations;

namespace SmartJobTracker.API.DTOs
{
    /// <summary>
    /// Data Transfer Object used when CREATING a new job - only contains fields the client should send
    /// Notice: No Id, CreatedAt, UpdatedAt - those are server-generated
    /// </summary>
    public class CreateJobDto
    {
        /// <summary>
        /// Company name is required - max 200 characters
        /// [Required] means API will reject request if this is missing
        /// [MaxLength] prevents storing huge strings in DB
        /// </summary>
        [Required(ErrorMessage = "Company name is required")]
        [MaxLength(200, ErrorMessage = "Company name cannot exceed 200 characters")]
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// Job title is required - max 200 characters
        /// </summary>
        [Required(ErrorMessage = "Job title is required")]
        [MaxLength(200, ErrorMessage = "Job title cannot exceed 200 characters")]
        public string JobTitle { get; set; } = string.Empty;

        /// <summary>
        /// Job URL is required - must be a valid URL format
        /// [Url] attribute validates the format automatically
        /// </summary>
        [Required(ErrorMessage = "Job URL is required")]
        [Url(ErrorMessage = "Please provide a valid URL")]
        [MaxLength(500, ErrorMessage = "URL cannot exceed 500 characters")]
        public string JobUrl { get; set; } = string.Empty;

        /// <summary>
        /// Full job description - required for AI gap analysis later
        /// </summary>
        [Required(ErrorMessage = "Job description is required")]
        public string JobDescription { get; set; } = string.Empty;

        /// <summary>
        /// Application status - defaults to "New" if not provided
        /// Valid values: New, Applied, Interview, Offer, Rejected
        /// </summary>
        public string Status { get; set; } = "New";

        /// <summary>
        /// Optional notes about the job - max 1000 characters
        /// </summary>
        [MaxLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        /// <summary>
        /// Date the job was found - defaults to today if not provided
        /// </summary>
        public DateTime DateFound { get; set; } = DateTime.UtcNow;
    }
}