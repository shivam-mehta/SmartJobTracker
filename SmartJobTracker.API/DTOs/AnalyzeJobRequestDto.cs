// AnalyzeJobRequestDto.cs
// Request body for the analyze endpoint.
// Keeps the resume text out of the URL and away from server logs.
using System.ComponentModel.DataAnnotations;
namespace SmartJobTracker.API.DTOs;

public class AnalyzeJobRequestDto
{
    [Required(ErrorMessage = "Resume text is required")]
    public string ResumeText { get; set; } = string.Empty;
}