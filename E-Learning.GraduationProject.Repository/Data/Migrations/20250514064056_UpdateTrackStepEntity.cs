using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning.GraduationProject.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrackStepEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PractiseProblems_TrackSteps_TrackStepId",
                table: "PractiseProblems");

            migrationBuilder.DropIndex(
                name: "IX_PractiseProblems_TrackStepId",
                table: "PractiseProblems");

            migrationBuilder.DropColumn(
                name: "TrackStepId",
                table: "PractiseProblems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackStepId",
                table: "PractiseProblems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PractiseProblems_TrackStepId",
                table: "PractiseProblems",
                column: "TrackStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_PractiseProblems_TrackSteps_TrackStepId",
                table: "PractiseProblems",
                column: "TrackStepId",
                principalTable: "TrackSteps",
                principalColumn: "Id");
        }
    }
}
