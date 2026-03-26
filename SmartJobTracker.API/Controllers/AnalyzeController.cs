using Microsoft.AspNetCore.Mvc;
using SmartJobTracker.API.DTOs;
using SmartJobTracker.API.Services;

namespace SmartJobTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyzeController : ControllerBase
    {
        private readonly IAIAnalysisService _aiService;
        private readonly ILogger<AnalyzeController> _logger;

        // Constructor - DI injects AI service and logger
        public AnalyzeController(IAIAnalysisService aiService, ILogger<AnalyzeController> logger)
        {
            _aiService = aiService;
            _logger = logger;
        }

        // POST /api/analyze/quick
        // Instant AI analysis - no DB save required
        // User pastes job description + resume text
        // Returns match score + gap analysis
        // User decides to save to tracker or discard
        [HttpPost("quick")]
        public async Task<IActionResult> QuickAnalyze([FromBody] QuickAnalyzeDto dto)
        {
            try
            {
                // Send to Azure OpenAI via Semantic Kernel
                var result = await _aiService.AnalyzeJobGapAsync(
                    dto.ResumeText,
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