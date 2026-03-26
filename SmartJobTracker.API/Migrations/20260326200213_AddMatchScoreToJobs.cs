using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartJobTracker.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMatchScoreToJobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "MatchScore",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchScore",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
