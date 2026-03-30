using Microsoft.AspNetCore.Mvc;
using SmartJobTracker.API.DTOs;
using SmartJobTracker.API.Models;
using SmartJobTracker.API.Repositories;

namespace SmartJobTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumesController : ControllerBase
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly ILogger<ResumesController> _logger;

        // Constructor - DI injects resume repository and logger
        public ResumesController(IResumeRepository resumeRepository, ILogger<ResumesController> logger)
        {
            _resumeRepository = resumeRepository;
            _logger = logger;
        }

        // GET /api/resumes
        // Returns all resume versions
        [HttpGet]
        public async Task<IActionResult> GetAllResumes()
        {
            var resumes = await _resumeRepository.GetAllResumesAsync();
            var response = resumes.Select(r => MapToResponseDto(r));
            return Ok(response);
        }

        // GET /api/resumes/5
        // Returns a single resume by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetResumeById(int id)
        {
            var resume = await _resumeRepository.GetResumeByIdAsync(id);
            if (resume == null) return NotFound();
            return Ok(MapToResponseDto(resume));
        }

        // GET /api/resumes/default
        // Returns the default resume used for quick analyze
        [HttpGet("default")]
        public async Task<IActionResult> GetDefaultResume()
        {
            var resume = await _resumeRepository.GetDefaultResumeAsync();
            if (resume == null) return NotFound("No default resume set");
            return Ok(MapToResponseDto(resume));
        }

        // POST /api/resumes
        // Creates a new resume version
        [HttpPost]
        public async Task<IActionResult> CreateResume([FromBody] CreateResumeDto dto)
        {
            // Map DTO to Resume model
            // Serialize lists to JSON for storage
            var resume = new Resume
            {
                VersionName = dto.VersionName,
                Summary = dto.Summary,
                Skills = dto.Skills,
                Experience = dto.Experience,
                Certifications = dto.Certifications,
                FullResumeText = dto.FullResumeText,
                IsDefault = dto.IsDefault
            };

            var created = await _resumeRepository.CreateResumeAsync(resume);
            return CreatedAtAction(nameof(GetResumeById),
                new { id = created.Id },
                MapToResponseDto(created));
        }

        // PUT /api/resumes/5
        // Updates an existing resume version
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResume(int id, [FromBody] CreateResumeDto dto)
        {
            var resume = new Resume
            {
                VersionName = dto.VersionName,
                Summary = dto.Summary,
                Skills = dto.Skills,
                Experience = dto.Experience,
                Certifications = dto.Certifications,
                FullResumeText = dto.FullResumeText,
                IsDefault = dto.IsDefault
            };

            var updated = await _resumeRepository.UpdateResumeAsync(id, resume);
            if (updated == null) return NotFound();
            return Ok(MapToResponseDto(updated));
        }

        // DELETE /api/resumes/5
        // Deletes a resume version
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResume(int id)
        {
            var result = await _resumeRepository.DeleteResumeAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // PUT /api/resumes/5/set-default
        // Sets a resume as the default for quick analyze
        [HttpPut("{id}/set-default")]
        public async Task<IActionResult> SetDefaultResume(int id)
        {
            var result = await _resumeRepository.SetDefaultResumeAsync(id);
            if (!result) return NotFound();
            return Ok(new { message = $"Resume {id} set as default" });
        }

        // Private helper - maps Resume model to ResumeResponseDto
        private static ResumeResponseDto MapToResponseDto(Resume resume)
        {
            return new ResumeResponseDto
            {
                Id = resume.Id,
                VersionName = resume.VersionName,
                Summary = resume.Summary,
                Skills = resume.Skills,
                Experience = resume.Experience,
                Certifications = resume.Certifications,
                FullResumeText = resume.FullResumeText,
                IsDefault = resume.IsDefault,
                CreatedAt = resume.CreatedAt,
                UpdatedAt = resume.UpdatedAt
            };
        }
    }
}