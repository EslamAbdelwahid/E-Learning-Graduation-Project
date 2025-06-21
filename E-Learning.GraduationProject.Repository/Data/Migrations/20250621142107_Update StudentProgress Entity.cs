using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning.GraduationProject.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudentProgressEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSpent",
                table: "StudentProgresses");

            migrationBuilder.RenameColumn(
                name: "LastWatchedDate",
                table: "StudentProgresses",
                newName: "EnrolledAt");

            migrationBuilder.RenameColumn(
                name: "CurrentPositionSeconds",
                table: "StudentProgresses",
                newName: "ProgressPercentage");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "StudentProgresses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "StudentProgresses");

            migrationBuilder.RenameColumn(
                name: "ProgressPercentage",
                table: "StudentProgresses",
                newName: "CurrentPositionSeconds");

            migrationBuilder.RenameColumn(
                name: "EnrolledAt",
                table: "StudentProgresses",
                newName: "LastWatchedDate");

            migrationBuilder.AddColumn<int>(
                name: "TimeSpent",
                table: "StudentProgresses",
                type: "int",
                nullable: true);
        }
    }
}
