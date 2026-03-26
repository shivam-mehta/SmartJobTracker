using SmartJobTracker.API.Models;
using SmartJobTracker.API.DTOs;

namespace SmartJobTracker.API.Repositories
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetAllJobsAsync();
        Task<Job?> GetJobByIdAsync(int id);
        Task<Job> CreateJobAsync(Job job);
        Task<Job?> UpdateJobAsync(int id, Job job);
        Task<bool> DeleteJobAsync(int id);

        // Get dashboard analytics summary
        Task<DashboardDto> GetDashboardSummaryAsync();
    }
}