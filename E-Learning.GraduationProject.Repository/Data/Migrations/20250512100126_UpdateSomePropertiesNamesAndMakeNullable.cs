using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning.GraduationProject.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSomePropertiesNamesAndMakeNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackSteps_SpecializationTracks_SpecializationTrackId",
                table: "TrackSteps");

            migrationBuilder.DropTable(
                name: "SpecializationTracks");

            migrationBuilder.RenameColumn(
                name: "SpecializationTrackId",
                table: "TrackSteps",
                newName: "TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_TrackSteps_SpecializationTrackId",
                table: "TrackSteps",
                newName: "IX_TrackSteps_TrackId");

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedCompletionWeeks = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TrackSteps_Tracks_TrackId",
                table: "TrackSteps",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackSteps_Tracks_TrackId",
                table: "TrackSteps");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.RenameColumn(
                name: "TrackId",
                table: "TrackSteps",
                newName: "SpecializationTrackId");

            migrationBuilder.RenameIndex(
                name: "IX_TrackSteps_TrackId",
                table: "TrackSteps",
                newName: "IX_TrackSteps_SpecializationTrackId");

            migrationBuilder.CreateTable(
                name: "SpecializationTracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedCompletionWeeks = table.Column<int>(type: "int", nullable: true),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecializationTracks", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TrackSteps_SpecializationTracks_SpecializationTrackId",
                table: "TrackSteps",
                column: "SpecializationTrackId",
                principalTable: "SpecializationTracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
