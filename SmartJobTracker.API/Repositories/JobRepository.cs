using Microsoft.EntityFrameworkCore;
using SmartJobTracker.API.Data;
using SmartJobTracker.API.Models;

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
            existing.Notes = job.Notes;
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
    }
}