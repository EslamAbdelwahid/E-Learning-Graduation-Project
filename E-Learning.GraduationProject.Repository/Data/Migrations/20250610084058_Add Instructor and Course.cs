using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning.GraduationProject.Repository.data.migrations
{
    /// <inheritdoc />
    public partial class AddInstructorandCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "StudentProgresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expertise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    TotalStudents = table.Column<int>(type: "int", nullable: false),
                    TotalCourses = table.Column<int>(type: "int", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructor_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: true),
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: true),
                    TrackId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Course_Instructor_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_ProgrammingLanguages_ProgrammingLanguageId",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "ProgrammingLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgresses_CourseId",
                table: "StudentProgresses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_InstructorId",
                table: "Course",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_ProgrammingLanguageId",
                table: "Course",
                column: "ProgrammingLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_TrackId",
                table: "Course",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_UserId",
                table: "Instructor",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProgresses_Course_CourseId",
                table: "StudentProgresses",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProgresses_Course_CourseId",
                table: "StudentProgresses");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropIndex(
                name: "IX_StudentProgresses_CourseId",
                table: "StudentProgresses");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "StudentProgresses");
        }
    }
}
