using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartJobTracker.API.Controllers;
using SmartJobTracker.API.DTOs;
using SmartJobTracker.API.Models;
using SmartJobTracker.API.Repositories;
using SmartJobTracker.API.Services;

namespace SmartJobTracker.Tests
{
    /// <summary>
    /// Unit tests for JobsController
    /// We use Moq to fake IJobRepository so tests never touch a real database
    /// Each test follows the AAA pattern: Arrange, Act, Assert
    /// Arrange = set up test data
    /// Act = call the method being tested
    /// Assert = verify the result is what we expected
    /// </summary>
    public class JobsControllerTests
    {
        // JobsControllerTests.cs — constructor section
        // _mockAiService follows the same Moq pattern as _mockRepo.
        // We don't set up any AI methods yet since existing tests don't call AnalyzeJob.
        // When we add the AnalyzeJob test later, we'll use _mockAiService.Setup(...) there.
        private readonly Mock<IJobRepository> _mockRepo;
        private readonly JobsController _controller;
        private readonly Mock<IAIAnalysisService> _mockAiService;

        public JobsControllerTests()
        {
            // Create a fake IJobRepository using Moq
            _mockRepo = new Mock<IJobRepository>();
            _mockAiService = new Mock<IAIAnalysisService>();

            // Inject the fake repository into the controller
            // Controller doesn't know or care it's a fake - DI in action!
            _controller = new JobsController(_mockRepo.Object, _mockAiService.Object);
        }

        // TEST 1 - GetAllJobs returns 200 OK with list of jobs
        [Fact]
        public async Task GetAllJobs_ReturnsOk_WithListOfJobs()
        {
            // Arrange - set up fake data the mock will return
            var fakeJobs = new List<Job>
            {
                new Job { Id = 1, CompanyName = "Microsoft", JobTitle = "Senior .NET Developer",
                    JobUrl = "https://microsoft.com/job/1", JobDescription = "Great job",
                    Status = "New", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow,
                    DateFound = DateTime.UtcNow },
                new Job { Id = 2, CompanyName = "Google", JobTitle = "Solutions Engineer",
                    JobUrl = "https://google.com/job/2", JobDescription = "Another great job",
                    Status = "Applied", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow,
                    DateFound = DateTime.UtcNow }
            };

            // Tell the mock: when GetAllJobsAsync() is called, return fakeJobs
            _mockRepo.Setup(r => r.GetAllJobsAsync()).ReturnsAsync(fakeJobs);

            // Act - call the actual controller method
            var result = await _controller.GetAllJobs();

            // Assert - verify we got 200 OK back
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        // TEST 2 - GetJobById returns 200 OK when job exists
        [Fact]
        public async Task GetJobById_ReturnsOk_WhenJobExists()
        {
            // Arrange
            var fakeJob = new Job
            {
                Id = 1,
                CompanyName = "Microsoft",
                JobTitle = "Senior .NET Developer",
                JobUrl = "https://microsoft.com/job/1",
                JobDescription = "Great job",
                Status = "New",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DateFound = DateTime.UtcNow
            };

            // Tell the mock: when GetJobByIdAsync(1) is called, return fakeJob
            _mockRepo.Setup(r => r.GetJobByIdAsync(1)).ReturnsAsync(fakeJob);

            // Act
            var result = await _controller.GetJobById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        // TEST 3 - GetJobById returns 404 when job does not exist
        [Fact]
        public async Task GetJobById_ReturnsNotFound_WhenJobDoesNotExist()
        {
            // Arrange - mock returns null when job not found
            _mockRepo.Setup(r => r.GetJobByIdAsync(999)).ReturnsAsync((Job?)null);

            // Act
            var result = await _controller.GetJobById(999);

            // Assert - verify we got 404 Not Found
            Assert.IsType<NotFoundResult>(result);
        }

        // TEST 4 - CreateJob returns 201 Created with new job
        [Fact]
        public async Task CreateJob_ReturnsCreated_WithNewJob()
        {
            // Arrange
            var createDto = new CreateJobDto
            {
                CompanyName = "Amazon",
                JobTitle = ".NET Developer",
                JobUrl = "https://amazon.com/job/3",
                JobDescription = "Great opportunity",
                Status = "New",
                DateFound = DateTime.UtcNow
            };

            var createdJob = new Job
            {
                Id = 3,
                CompanyName = createDto.CompanyName,
                JobTitle = createDto.JobTitle,
                JobUrl = createDto.JobUrl,
                JobDescription = createDto.JobDescription,
                Status = createDto.Status,
                DateFound = createDto.DateFound,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Tell mock: when CreateJobAsync is called with any Job, return createdJob
            _mockRepo.Setup(r => r.CreateJobAsync(It.IsAny<Job>())).ReturnsAsync(createdJob);

            // Act
            var result = await _controller.CreateJob(createDto);

            // Assert - verify we got 201 Created
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(createdResult.Value);
        }

        // TEST 5 - DeleteJob returns 204 No Content when job exists
        [Fact]
        public async Task DeleteJob_ReturnsNoContent_WhenJobExists()
        {
            // Arrange - mock returns true (job was found and deleted)
            _mockRepo.Setup(r => r.DeleteJobAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteJob(1);

            // Assert - verify we got 204 No Content
            Assert.IsType<NoContentResult>(result);
        }

        // TEST 6 - DeleteJob returns 404 when job does not exist
        [Fact]
        public async Task DeleteJob_ReturnsNotFound_WhenJobDoesNotExist()
        {
            // Arrange - mock returns false (job not found)
            _mockRepo.Setup(r => r.DeleteJobAsync(999)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteJob(999);

            // Assert - verify we got 404 Not Found
            Assert.IsType<NotFoundResult>(result);
        }
    }
}