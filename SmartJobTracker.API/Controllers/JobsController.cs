using Microsoft.AspNetCore.Mvc;
using SmartJobTracker.API.Models;
using SmartJobTracker.API.Repositories;

namespace SmartJobTracker.API.Controllers
{
    [Route("api/[controller]")]        // Base route: /api/jobs
    [ApiController]                     // Enables automatic model validation and API behaviors
    public class JobsController : ControllerBase  // ControllerBase = API only, no views
    {
        // DI in action - .NET hands us IJobRepository automatically
        // Controller doesn't know or care HOW data is fetched - just that it gets fetched
        private readonly IJobRepository _jobRepository;

        // Constructor - this is where DI "injects" the repository
        public JobsController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        // GET /api/jobs
        // Returns all jobs ordered by newest first
        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _jobRepository.GetAllJobsAsync();
            return Ok(jobs);  // HTTP 200 + jobs as JSON
        }

        // GET /api/jobs/5
        // Returns a single job by ID
        // {id} in route matches the int id parameter automatically
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _jobRepository.GetJobByIdAsync(id);
            if (job == null) return NotFound();  // HTTP 404 if not found
            return Ok(job);                       // HTTP 200 + job as JSON
        }

        // POST /api/jobs
        // Creates a new job - receives job data from request body as JSON
        // [FromBody] tells .NET to deserialize the JSON request body into a Job object
        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] Job job)
        {
            var created = await _jobRepository.CreateJobAsync(job);

            // HTTP 201 Created + Location header pointing to the new job
            // e.g. Location: /api/jobs/1
            return CreatedAtAction(nameof(GetJobById), new { id = created.Id }, created);
        }

        // PUT /api/jobs/5
        // Updates an existing job by ID
        // Receives updated job data from request body as JSON
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] Job job)
        {
            var updated = await _jobRepository.UpdateJobAsync(id, job);
            if (updated == null) return NotFound();  // HTTP 404 if job doesn't exist
            return Ok(updated);                       // HTTP 200 + updated job as JSON
        }

        // DELETE /api/jobs/5
        // Deletes a job by ID
        // Returns 204 No Content on success - standard REST convention for deletes
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var result = await _jobRepository.DeleteJobAsync(id);
            if (!result) return NotFound();  // HTTP 404 if job doesn't exist
            return NoContent();              // HTTP 204 - deleted successfully, nothing to return
        }
    }
}