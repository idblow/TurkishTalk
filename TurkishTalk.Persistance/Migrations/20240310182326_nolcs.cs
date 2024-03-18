using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurkishTalk.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class nolcs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionText",
                table: "ReadTask");

            migrationBuilder.RenameColumn(
                name: "QuestionsData",
                table: "ReadTask",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "TestData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadTaskId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestData_ReadTask_ReadTaskId",
                        column: x => x.ReadTaskId,
                        principalTable: "ReadTask",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestData_ReadTaskId",
                table: "TestData",
                column: "ReadTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestData");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ReadTask",
                newName: "QuestionsData");

            migrationBuilder.AddColumn<string>(
                name: "QuestionText",
                table: "ReadTask",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
