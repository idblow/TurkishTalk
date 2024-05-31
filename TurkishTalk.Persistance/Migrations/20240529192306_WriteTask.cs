using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurkishTalk.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class WriteTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionText",
                table: "ReadTask");

            migrationBuilder.AlterColumn<string>(
                name: "FixStringCorrect",
                table: "WriteTask",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FixString",
                table: "WriteTask",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FixStringCorrect",
                table: "WriteTask",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FixString",
                table: "WriteTask",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "QuestionText",
                table: "ReadTask",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
