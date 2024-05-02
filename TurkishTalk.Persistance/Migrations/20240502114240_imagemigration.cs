using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurkishTalk.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class imagemigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "User",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ImageContentType",
                table: "User");
        }
    }
}
