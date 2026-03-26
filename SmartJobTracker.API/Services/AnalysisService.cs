using Microsoft.SemanticKernel;
using System.Text.Json;

namespace SmartJobTracker.API.Services
{
    /// <summary>
    /// AI-powered job analysis service using Semantic Kernel + Azure OpenAI
    /// Implements IAIAnalysisService - returns structured GapAnalysisResult
    /// </summary>
    public class AnalysisService : IAIAnalysisService
    {
        private readonly Kernel _kernel;
        private readonly ILogger<AnalysisService> _logger;

        

        // Constructor - Kernel and Logger injected via DI
        public AnalysisService(Kernel kernel, ILogger<AnalysisService> logger)
        {
            _kernel = kernel;
            _logger = logger;
        }

        public async Task<GapAnalysisResult> AnalyzeJobGapAsync(string resumeText, string jobDescription)
        {
            try
            {
                // Prompt engineering - structured output request
                // $$""" allows {{variable}} interpolation inside raw string
                var prompt = $$"""
                    You are an expert technical recruiter and career coach.
                    
                    Analyze the following job description against the candidate's resume
                    and provide a detailed gap analysis.
                    
                    JOB DESCRIPTION:
                    {{jobDescription}}
                    
                    CANDIDATE RESUME:
                    {{resumeText}}
                    
                    Return ONLY valid JSON in exactly this format, no other text:
                    {
                        "matchScore": <0-100 number>,
                        "matchingSkills": ["skill1", "skill2"],
                        "missingSkills": ["skill1", "skill2"],
                        "suggestedKeywords": ["keyword1", "keyword2"],
                        "recommendation": "Brief overall assessment"
                    }
                    """;

                // Send prompt to Azure OpenAI via Semantic Kernel
                var result = await _kernel.InvokePromptAsync(prompt);
                var jsonResponse = result.ToString();

                // Deserialize JSON response into GapAnalysisResult object
                // JsonSerializerOptions - case insensitive to handle AI response variations
                var analysisResult = JsonSerializer.Deserialize<GapAnalysisResult>(
                    jsonResponse,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return analysisResult ?? new GapAnalysisResult
                {
                    Recommendation = "Analysis failed - please try again",
                    MatchScore = 0
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error analyzing job gap");
                throw;
            }
        }
    }
}