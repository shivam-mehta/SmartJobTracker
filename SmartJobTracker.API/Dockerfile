# Stage 1 - Build stage
# Use the official .NET 10 SDK image to build our app
# SDK image is larger but has everything needed to compile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# Set working directory inside the container
WORKDIR /src

# Copy the project file first and restore dependencies
# This layer is cached - only re-runs if .csproj changes
COPY ["SmartJobTracker.API.csproj", "."]
RUN dotnet restore

# Copy all remaining source code
COPY . .

# Build and publish in Release mode to /app/publish folder
RUN dotnet publish -c Release -o /app/publish

# Stage 2 - Runtime stage
# Use smaller ASP.NET runtime image - no SDK needed to run
# This keeps our final image lean and secure
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

# Set working directory
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/publish .

# Tell Docker our app listens on port 8080
EXPOSE 8080

# Set environment to Production
ENV ASPNETCORE_ENVIRONMENT=Production

# Command to run when container starts
ENTRYPOINT ["dotnet", "SmartJobTracker.API.dll"]