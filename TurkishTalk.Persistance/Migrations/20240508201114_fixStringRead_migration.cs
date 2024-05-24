using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurkishTalk.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class fixStringRead_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestData");

            migrationBuilder.DropColumn(
                name: "FixString",
                table: "ReadTask");

            migrationBuilder.RenameColumn(
                name: "FixStringCorrect",
                table: "ReadTask",
                newName: "VoiceExampleMimeType");

            migrationBuilder.AlterColumn<string>(
                name: "Rule",
                table: "WriteTask",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Tests",
                table: "WriteTask",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Word",
                table: "WordDictionary",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TextReadingExample",
                table: "ReadTask",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Tests",
                table: "ReadTask",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "VoiceExample",
                table: "ReadTask",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<string>(
                name: "Rule",
                table: "GrammarTask",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tests",
                table: "WriteTask");

            migrationBuilder.DropColumn(
                name: "Tests",
                table: "ReadTask");

            migrationBuilder.DropColumn(
                name: "VoiceExample",
                table: "ReadTask");

            migrationBuilder.RenameColumn(
                name: "VoiceExampleMimeType",
                table: "ReadTask",
                newName: "FixStringCorrect");

            migrationBuilder.AlterColumn<string>(
                name: "Rule",
                table: "WriteTask",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Word",
                table: "WordDictionary",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "TextReadingExample",
                table: "ReadTask",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "FixString",
                table: "ReadTask",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Rule",
                table: "GrammarTask",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "TestData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadTaskId = table.Column<int>(type: "int", nullable: true),
                    WriteTaskId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestData_ReadTask_ReadTaskId",
                        column: x => x.ReadTaskId,
                        principalTable: "ReadTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TestData_WriteTask_WriteTaskId",
                        column: x => x.WriteTaskId,
                        principalTable: "WriteTask",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestData_ReadTaskId",
                table: "TestData",
                column: "ReadTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TestData_WriteTaskId",
                table: "TestData",
                column: "WriteTaskId");
        }
    }
}
