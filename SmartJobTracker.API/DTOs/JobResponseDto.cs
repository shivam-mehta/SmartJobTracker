namespace SmartJobTracker.API.DTOs
{
    /// <summary>
    /// DTO used when RETURNING job data to the client
    /// This is what the API sends back in responses
    /// We control exactly what the client sees - no internal DB fields exposed
    /// Think of it as the "public face" of your Job data
    /// </summary>
    public class JobResponseDto
    {
        /// <summary>
        /// Job ID - client needs this to make update/delete requests
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// Job title
        /// </summary>
        public string JobTitle { get; set; } = string.Empty;

        /// <summary>
        /// Job posting URL
        /// </summary>
        public string JobUrl { get; set; } = string.Empty;

        /// <summary>
        /// Full job description
        /// </summary>
        public string JobDescription { get; set; } = string.Empty;

        /// <summary>
        /// Current application status
        /// New, Applied, Interview, Offer, Rejected
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Optional notes about the job
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Date job was found
        /// </summary>
        public DateTime DateFound { get; set; }

        /// <summary>
        /// Date application was submitted - null if not yet applied
        /// </summary>
        public DateTime? DateApplied { get; set; }

        /// <summary>
        /// When this record was created in our system
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// When this record was last updated
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}