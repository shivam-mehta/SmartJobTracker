// IAIAnalysisService.cs
// Defines the contract for AI analysis — any AI provider (Gemini, Azure OpenAI, etc.)
// must implement this interface. GapAnalysisResult is the structured response object
// that the controller and eventually the React dashboard will consume.

namespace SmartJobTracker.API.Services;

public interface IAIAnalysisService
{
    Task<GapAnalysisResult> AnalyzeJobGapAsync(string resumeText, string jobDescription);
}

public class GapAnalysisResult
{
    // Skills found in both resume and job description
    public List<string> MatchingSkills { get; set; } = new();

    // Skills in job description but missing from resume — the gap
    public List<string> MissingSkills { get; set; } = new();

    // Keywords AI recommends adding to resume to improve ATS scoring
    public List<string> SuggestedKeywords { get; set; } = new();

    // 0-100 score: how well the resume matches this job
    public int MatchScore { get; set; }

    // Human-readable summary: e.g. "Strong match, consider adding Azure DevOps"
    public string Recommendation { get; set; } = string.Empty;
}