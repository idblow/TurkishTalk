using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurkishTalk.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class vnov : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transcription",
                table: "WordDictionary");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AlfabetTask",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AlfabetTask");

            migrationBuilder.AddColumn<string>(
                name: "Transcription",
                table: "WordDictionary",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
