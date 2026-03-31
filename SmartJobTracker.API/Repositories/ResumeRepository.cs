using Microsoft.EntityFrameworkCore;
using SmartJobTracker.API.Data;
using SmartJobTracker.API.Models;

namespace SmartJobTracker.API.Repositories
{
    /// <summary>
    /// Repository implementation for resume CRUD operations using EF Core
    /// Supports multiple resume versions with default selection
    /// </summary>
    public class ResumeRepository : IResumeRepository
    {
        private readonly AppDbContext _context;

        // Constructor - EF Core DbContext injected via DI
        public ResumeRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all resume versions ordered by newest first
        public async Task<IEnumerable<Resume>> GetAllResumesAsync()
        {
            return await _context.Resumes
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        // Get a single resume by ID
        public async Task<Resume?> GetResumeByIdAsync(int id)
        {
            return await _context.Resumes.FindAsync(id);
        }

        // Get the default resume for quick analyze
        public async Task<Resume?> GetDefaultResumeAsync()
        {
            return await _context.Resumes
                .FirstOrDefaultAsync(r => r.IsDefault);
        }

        // Create a new resume version
        public async Task<Resume> CreateResumeAsync(Resume resume)
        {
            // If this is set as default, unset all others first
            if (resume.IsDefault)
                await UnsetAllDefaultsAsync();

            resume.CreatedAt = DateTime.UtcNow;
            resume.UpdatedAt = DateTime.UtcNow;
            _context.Resumes.Add(resume);
            await _context.SaveChangesAsync();
            return resume;
        }

        // Update an existing resume version
        public async Task<Resume?> UpdateResumeAsync(int id, Resume resume)
        {
            var existing = await _context.Resumes.FindAsync(id);
            if (existing == null) return null;

            // If setting as default, unset all others first
            if (resume.IsDefault && !existing.IsDefault)
                await UnsetAllDefaultsAsync();

            existing.VersionName = resume.VersionName;
            existing.Summary = resume.Summary;
            existing.SkillsJson = resume.SkillsJson;
            existing.ExperienceJson = resume.ExperienceJson;
            existing.CertificationsJson = resume.CertificationsJson;
            existing.FullResumeText = resume.FullResumeText;
            existing.IsDefault = resume.IsDefault;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        // Delete a resume version
        public async Task<bool> DeleteResumeAsync(int id)
        {
            var resume = await _context.Resumes.FindAsync(id);
            if (resume == null) return false;

            _context.Resumes.Remove(resume);
            await _context.SaveChangesAsync();
            return true;
        }

        // Set a resume as the default
        // Unsets all others first - only one default at a time
        public async Task<bool> SetDefaultResumeAsync(int id)
        {
            var resume = await _context.Resumes.FindAsync(id);
            if (resume == null) return false;

            await UnsetAllDefaultsAsync();
            resume.IsDefault = true;
            resume.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        // Private helper - unsets all default resumes
        private async Task UnsetAllDefaultsAsync()
        {
            var defaults = await _context.Resumes
                .Where(r => r.IsDefault)
                .ToListAsync();
            defaults.ForEach(r => r.IsDefault = false);
            await _context.SaveChangesAsync();
        }
    }
}