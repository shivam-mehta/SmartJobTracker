using SmartJobTracker.API.Models;

namespace SmartJobTracker.API.Repositories
{
    /// <summary>
    /// Repository interface for resume CRUD operations
    /// Supports multiple resume versions with default selection
    /// </summary>
    public interface IResumeRepository
    {
        // Get all resume versions
        Task<IEnumerable<Resume>> GetAllResumesAsync();

        // Get a single resume by ID
        Task<Resume?> GetResumeByIdAsync(int id);

        // Get the default resume for quick analyze
        Task<Resume?> GetDefaultResumeAsync();

        // Create a new resume version
        Task<Resume> CreateResumeAsync(Resume resume);

        // Update an existing resume version
        Task<Resume?> UpdateResumeAsync(int id, Resume resume);

        // Delete a resume version
        Task<bool> DeleteResumeAsync(int id);

        // Set a resume as the default
        // Unsets all others first - only one default at a time
        Task<bool> SetDefaultResumeAsync(int id);
    }
}