using Microsoft.AspNetCore.Mvc;
using SmartJobTracker.API.DTOs;
using SmartJobTracker.API.Repositories;
using SmartJobTracker.API.Services;

namespace SmartJobTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyzeController : ControllerBase
    {
        private readonly IAIAnalysisService _aiService;
        private readonly IResumeRepository _resumeRepository;
        private readonly ILogger<AnalyzeController> _logger;

        // Constructor - DI injects AI service, resume repository and logger
        public AnalyzeController(
            IAIAnalysisService aiService,
            IResumeRepository resumeRepository,
            ILogger<AnalyzeController> logger)
        {
            _aiService = aiService;
            _resumeRepository = resumeRepository;
            _logger = logger;
        }

        // POST /api/analyze/quick
        // Instant AI analysis - no DB save required
        // Auto-loads default resume if resumeText not provided
        // User decides to save to tracker or discard after seeing score
        [HttpPost("quick")]
        public async Task<IActionResult> QuickAnalyze([FromBody] QuickAnalyzeDto dto)
        {
            try
            {
                var resumeText = dto.ResumeText;

                // If no resume text provided - auto load default resume
                // This is the magic - user never has to paste resume again!
                if (string.IsNullOrWhiteSpace(resumeText))
                {
                    var defaultResume = await _resumeRepository.GetDefaultResumeAsync();
                    if (defaultResume == null)
                        return BadRequest("No resume text provided and no default resume found. Please add a resume first.");

                    // Use full resume text for richest AI analysis
                    resumeText = defaultResume.FullResumeText;
                }

                // Send to Azure OpenAI via Semantic Kernel
                var result = await _aiService.AnalyzeJobGapAsync(
                    resumeText,
                    dto.JobDescription);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during quick analysis");
                return StatusCode(500, "Analysis failed — please try again");
            }
        }
    }
}