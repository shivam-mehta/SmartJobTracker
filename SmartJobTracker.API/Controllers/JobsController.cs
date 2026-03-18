using Microsoft.AspNetCore.Mvc;
using SmartJobTracker.API.DTOs;
using SmartJobTracker.API.Models;
using SmartJobTracker.API.Repositories;

namespace SmartJobTracker.API.Controllers
{
    [Route("api/[controller]")]        // Base route: /api/jobs
    [ApiController]                     // Enables automatic model validation and API behaviors
    public class JobsController : ControllerBase
    {
        // DI in action - .NET hands us IJobRepository automatically
        private readonly IJobRepository _jobRepository;

        // Constructor - DI injects the repository here
        public JobsController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        // GET /api/jobs
        // Returns all jobs as a list of JobResponseDto
        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _jobRepository.GetAllJobsAsync();

            // Map each Job model to JobResponseDto
            // We never return raw DB models to the client
            var response = jobs.Select(job => MapToResponseDto(job));
            return Ok(response);
        }

        // GET /api/jobs/5
        // Returns a single job by ID as JobResponseDto
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _jobRepository.GetJobByIdAsync(id);
            if (job == null) return NotFound();

            // Map Job model to JobResponseDto before returning
            return Ok(MapToResponseDto(job));
        }

        // POST /api/jobs
        // Accepts CreateJobDto - never accepts raw Job model from client
        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobDto dto)
        {
            // [ApiController] automatically returns 400 if validation fails
            // e.g. if CompanyName is missing, Required attribute catches it

            // Map CreateJobDto to Job model for database storage
            var job = new Job
            {
                CompanyName = dto.CompanyName,
                JobTitle = dto.JobTitle,
                JobUrl = dto.JobUrl,
                JobDescription = dto.JobDescription,
                Status = dto.Status,
                Notes = dto.Notes,
                DateFound = dto.DateFound
            };

            var created = await _jobRepository.CreateJobAsync(job);

            // Return 201 Created + location header + JobResponseDto
            return CreatedAtAction(nameof(GetJobById),
                new { id = created.Id },
                MapToResponseDto(created));
        }

        // PUT /api/jobs/5
        // Accepts UpdateJobDto - includes status and dateApplied
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] UpdateJobDto dto)
        {
            // Map UpdateJobDto to Job model
            var job = new Job
            {
                CompanyName = dto.CompanyName,
                JobTitle = dto.JobTitle,
                JobUrl = dto.JobUrl,
                JobDescription = dto.JobDescription,
                Status = dto.Status,
                Notes = dto.Notes,
                DateFound = dto.DateFound,
                DateApplied = dto.DateApplied
            };

            var updated = await _jobRepository.UpdateJobAsync(id, job);
            if (updated == null) return NotFound();

            // Return updated job as JobResponseDto
            return Ok(MapToResponseDto(updated));
        }

        // DELETE /api/jobs/5
        // Deletes a job by ID - returns 204 No Content on success
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var result = await _jobRepository.DeleteJobAsync(id);
            if (!result) return NotFound();
            return NoContent();  // HTTP 204 - deleted successfully
        }

        // Private helper method - maps Job model to JobResponseDto
        // Private because it's only used inside this controller
        // We'll move this to a dedicated Mapper class in a future phase
        private static JobResponseDto MapToResponseDto(Job job)
        {
            return new JobResponseDto
            {
                Id = job.Id,
                CompanyName = job.CompanyName,
                JobTitle = job.JobTitle,
                JobUrl = job.JobUrl,
                JobDescription = job.JobDescription,
                Status = job.Status,
                Notes = job.Notes,
                DateFound = job.DateFound,
                DateApplied = job.DateApplied,
                CreatedAt = job.CreatedAt,
                UpdatedAt = job.UpdatedAt
            };
        }
    }
}