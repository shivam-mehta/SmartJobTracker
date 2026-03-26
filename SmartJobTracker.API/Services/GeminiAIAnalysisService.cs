// GeminiAIAnalysisService.cs
// Implements IAIAnalysisService using Google Gemini REST API via HttpClient.
// Sends a structured prompt asking Gemini to compare resume vs job description
// and return a strict JSON response — no free text — so we can deserialize cleanly.
// Provider-agnostic: controller never knows this is Gemini specifically.

using System.Text;
using System.Text.Json;

namespace SmartJobTracker.API.Services;

public class GeminiAIAnalysisService : IAIAnalysisService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GeminiAIAnalysisService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        // Reads from User Secrets locally, environment variable in production
        _apiKey = configuration["Gemini:ApiKey"]
            ?? throw new InvalidOperationException("Gemini:ApiKey is not configured.");
    }

    public async Task<GapAnalysisResult> AnalyzeJobGapAsync(string resumeText, string jobDescription)
    {
        // Split into two parts to avoid raw string interpolation conflicts with JSON braces.
        // jsonTemplate uses regular string — no interpolation needed there.
        // resumeText and jobDescription are concatenated separately below.
        var jsonTemplate = @"{
          ""matchingSkills"": [""skill1"", ""skill2""],
          ""missingSkills"": [""skill1"", ""skill2""],
          ""suggestedKeywords"": [""keyword1"", ""keyword2""],
          ""matchScore"": 75,
          ""recommendation"": ""One sentence summary here.""
        }";
        var prompt = $"You are a resume gap analyzer. Compare the resume and job description below.\n" +
                     $"Respond ONLY with a valid JSON object — no markdown, no explanation, no backticks.\n" +
                     $"Use exactly this structure:\n{jsonTemplate}\n\n" +
                     $"RESUME:\n{resumeText}\n\n" +
                     $"JOB DESCRIPTION:\n{jobDescription}";

        // Gemini REST API request body structure
        var requestBody = new
        {
            contents = new[]
            {
                new { parts = new[] { new { text = prompt } } }
            }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Gemini 2.0 Flash — fast and free tier friendly
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash-001:generateContent?key={_apiKey}";

        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        // Navigate Gemini response envelope to extract the text content
        using var doc = JsonDocument.Parse(responseBody);
        var text = doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString() ?? "{}";

        // Deserialize into our GapAnalysisResult — camelCase from Gemini maps to PascalCase here
        var result = JsonSerializer.Deserialize<GapAnalysisResult>(text,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result ?? new GapAnalysisResult();
    }
}