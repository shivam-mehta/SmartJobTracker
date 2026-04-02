
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using SmartJobTracker.API.Data;
using SmartJobTracker.API.Repositories;
using SmartJobTracker.API.Services;

namespace SmartJobTracker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.            
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Add Swagger UI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Add database
            // Use Azure SQL in production, local SQL in development
            var connectionString = builder.Environment.IsDevelopment()
                ? builder.Configuration.GetConnectionString("DefaultConnection")
                : builder.Configuration.GetConnectionString("AzureSqlConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                connectionString,
                sqlOptions => sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null
                )
            ));

            // Register repositories - Dependency Injection will inject
            // JobRepository whenever IJobRepository is requested
            builder.Services.AddScoped<IJobRepository, JobRepository>();

            // Register Resume Repository
            builder.Services.AddScoped<IResumeRepository, ResumeRepository>();

            // Register HttpClient for GeminiAIAnalysisService — typed client pattern.
            // AddHttpClient wires up a dedicated HttpClient instance for this service.
            // IAIAnalysisService is the interface — swap to OllamaAIAnalysisService or
            // AzureOpenAIAnalysisService later just by changing this one line.
            //builder.Services.AddHttpClient<IAIAnalysisService, GeminiAIAnalysisService>();


            // Register Semantic Kernel with Azure OpenAI
            // Kernel is registered as Singleton - one instance for entire app lifetime
            builder.Services.AddSingleton<Kernel>(sp => {
                var config = sp.GetRequiredService<IConfiguration>();
                var kernel = Kernel.CreateBuilder()
                    .AddAzureOpenAIChatCompletion(
                        deploymentName: config["AzureOpenAI:DeploymentName"]!,
                        endpoint: config["AzureOpenAI:Endpoint"]!,
                        apiKey: config["AzureOpenAI:ApiKey"]!)
                    .Build();
                return kernel;
            });

            // Register Analysis Service - uses Kernel to talk to Azure OpenAI
            builder.Services.AddScoped<IAIAnalysisService, AnalysisService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            // HTTPS redirection disabled for container deployment
            // Azure Container Apps handles HTTPS at the ingress level
            // app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
