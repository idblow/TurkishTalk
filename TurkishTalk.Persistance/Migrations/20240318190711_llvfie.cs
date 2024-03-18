using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurkishTalk.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class llvfie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestData_GrammarTask_GrammarTaskId",
                table: "TestData");

            migrationBuilder.DropIndex(
                name: "IX_TestData_GrammarTaskId",
                table: "TestData");

            migrationBuilder.DropColumn(
                name: "GrammarTaskId",
                table: "TestData");

            migrationBuilder.AddColumn<string>(
                name: "RadioTests",
                table: "GrammarTask",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tests",
                table: "GrammarTask",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RadioTests",
                table: "GrammarTask");

            migrationBuilder.DropColumn(
                name: "Tests",
                table: "GrammarTask");

            migrationBuilder.AddColumn<int>(
                name: "GrammarTaskId",
                table: "TestData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestData_GrammarTaskId",
                table: "TestData",
                column: "GrammarTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestData_GrammarTask_GrammarTaskId",
                table: "TestData",
                column: "GrammarTaskId",
                principalTable: "GrammarTask",
                principalColumn: "Id");
        }
    }
}
