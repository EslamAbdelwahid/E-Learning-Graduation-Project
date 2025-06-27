using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning.GraduationProject.Repository.data.migrations
{
    /// <inheritdoc />
    public partial class AddOrderIntoStudentProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "StudentProgresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgresses_OrderId",
                table: "StudentProgresses",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProgresses_Orders_OrderId",
                table: "StudentProgresses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProgresses_Orders_OrderId",
                table: "StudentProgresses");

            migrationBuilder.DropIndex(
                name: "IX_StudentProgresses_OrderId",
                table: "StudentProgresses");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "StudentProgresses");
        }
    }
}
