using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurkishTalk.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class swdnj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WriteTask",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GrammarTaskId",
                table: "TestData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WriteTaskId",
                table: "TestData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuestionText",
                table: "ReadTask",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GrammarTask",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TestData_GrammarTaskId",
                table: "TestData",
                column: "GrammarTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TestData_WriteTaskId",
                table: "TestData",
                column: "WriteTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestData_GrammarTask_GrammarTaskId",
                table: "TestData",
                column: "GrammarTaskId",
                principalTable: "GrammarTask",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestData_WriteTask_WriteTaskId",
                table: "TestData",
                column: "WriteTaskId",
                principalTable: "WriteTask",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestData_GrammarTask_GrammarTaskId",
                table: "TestData");

            migrationBuilder.DropForeignKey(
                name: "FK_TestData_WriteTask_WriteTaskId",
                table: "TestData");

            migrationBuilder.DropIndex(
                name: "IX_TestData_GrammarTaskId",
                table: "TestData");

            migrationBuilder.DropIndex(
                name: "IX_TestData_WriteTaskId",
                table: "TestData");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WriteTask");

            migrationBuilder.DropColumn(
                name: "GrammarTaskId",
                table: "TestData");

            migrationBuilder.DropColumn(
                name: "WriteTaskId",
                table: "TestData");

            migrationBuilder.DropColumn(
                name: "QuestionText",
                table: "ReadTask");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GrammarTask");
        }
    }
}
