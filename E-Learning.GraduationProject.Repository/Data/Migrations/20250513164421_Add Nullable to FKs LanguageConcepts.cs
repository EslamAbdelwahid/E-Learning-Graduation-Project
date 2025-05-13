using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning.GraduationProject.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNullabletoFKsLanguageConcepts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanguageConcepts_ProgrammingLanguages_ProgrammingLanguageId",
                table: "LanguageConcepts");

            migrationBuilder.AlterColumn<int>(
                name: "ProgrammingLanguageId",
                table: "LanguageConcepts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageConcepts_ProgrammingLanguages_ProgrammingLanguageId",
                table: "LanguageConcepts",
                column: "ProgrammingLanguageId",
                principalTable: "ProgrammingLanguages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanguageConcepts_ProgrammingLanguages_ProgrammingLanguageId",
                table: "LanguageConcepts");

            migrationBuilder.AlterColumn<int>(
                name: "ProgrammingLanguageId",
                table: "LanguageConcepts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageConcepts_ProgrammingLanguages_ProgrammingLanguageId",
                table: "LanguageConcepts",
                column: "ProgrammingLanguageId",
                principalTable: "ProgrammingLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
