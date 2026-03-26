using Microsoft.EntityFrameworkCore;
using SmartJobTracker.API.Data;
using SmartJobTracker.API.Models;
using SmartJobTracker.API.DTOs;
using Dapper;

namespace SmartJobTracker.API.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly AppDbContext _context;

        public JobRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            return await _context.Jobs
                .OrderByDescending(j => j.CreatedAt)
                .ToListAsync();
        }

        public async Task<Job?> GetJobByIdAsync(int id)
        {
            return await _context.Jobs.FindAsync(id);
        }

        public async Task<Job> CreateJobAsync(Job job)
        {
            job.CreatedAt = DateTime.UtcNow;
            job.UpdatedAt = DateTime.UtcNow;
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<Job?> UpdateJobAsync(int id, Job job)
        {
            var existing = await _context.Jobs.FindAsync(id);
            if (existing == null) return null;

            existing.CompanyName = job.CompanyName;
            existing.JobTitle = job.JobTitle;
            existing.JobUrl = job.JobUrl;
            existing.JobDescription = job.JobDescription;
            existing.Status = job.Status;
            existing.DateApplied = job.DateApplied;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteJobAsync(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return false;

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get dashboard analytics summary using Dapper for performance
        public async Task<DashboardDto> GetDashboardSummaryAsync()
        {
            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();
            // Dapper query to get summary counts and averages
            var sql = @"SELECT 
                COUNT(*) AS TotalJobs,
                SUM(CASE WHEN Status = 'New' THEN 1 ELSE 0 END) AS NewJobs,
                SUM(CASE WHEN Status = 'Applied' THEN 1 ELSE 0 END) AS AppliedJobs,
                SUM(CASE WHEN Status = 'Interview' THEN 1 ELSE 0 END) AS InterviewJobs,
                SUM(CASE WHEN Status = 'Offer' THEN 1 ELSE 0 END) AS OfferJobs,
                SUM(CASE WHEN Status = 'Rejected' THEN 1 ELSE 0 END) AS RejectedJobs,
                AVG(CAST(MatchScore AS FLOAT)) AS AverageMatchScore,
                SUM(CASE WHEN DATEDIFF(day, DateFound, GETUTCDATE()) >= 7 THEN 1 ELSE 0 END) AS JobsOlderThan7Days,
                SUM(CASE WHEN DATEDIFF(day, DateFound, GETUTCDATE()) >= 14 THEN 1 ELSE 0 END) AS JobsOlderThan14Days,
                SUM(CASE WHEN MatchScore >= 90 THEN 1 ELSE 0 END) AS GoldJobs,
                SUM(CASE WHEN MatchScore >= 80 AND MatchScore < 90 THEN 1 ELSE 0 END) AS BlueJobs,
                SUM(CASE WHEN MatchScore >= 70 AND MatchScore < 80 THEN 1 ELSE 0 END) AS GreenJobs,
                SUM(CASE WHEN MatchScore >= 60 AND MatchScore < 70 THEN 1 ELSE 0 END) AS YellowJobs,
                SUM(CASE WHEN MatchScore < 60 THEN 1 ELSE 0 END) AS RedJobs
            FROM Jobs";
            var result = await connection.QuerySingleAsync<DashboardDto>(sql);
            return result;
        }
    }
}