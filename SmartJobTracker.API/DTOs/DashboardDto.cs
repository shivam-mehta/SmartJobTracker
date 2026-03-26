namespace SmartJobTracker.API.DTOs
{
    /// <summary>
    /// Dashboard analytics DTO - summary of all jobs for the dashboard
    /// Used by React dashboard to display stats, pipeline counts and warnings
    /// </summary>
    public class DashboardDto
    {
        // Dashboard analytics DTO - summary of all jobs for the dashboard
        public int TotalJobs { get; set; }
        public int NewJobs { get; set; }
        public int AppliedJobs { get; set; }
        public int InterviewJobs { get; set; }
        public int OfferJobs { get; set; }
        public int RejectedJobs { get; set; }
        public double AverageMatchScore { get; set; }
        public int JobsOlderThan7Days { get; set; }
        public int JobsOlderThan14Days { get; set; }

        // Color breakdown for match score chart
        public int GoldJobs { get; set; }    // 90%+
        public int BlueJobs { get; set; }    // 80-90%
        public int GreenJobs { get; set; }   // 70-80%
        public int YellowJobs { get; set; }  // 60-70%
        public int RedJobs { get; set; }     // <60%
    }
}