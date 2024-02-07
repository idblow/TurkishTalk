using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurkishTalk.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeshedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlfabetTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    Tests = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlfabetTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlfabetTask_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrammarTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    level = table.Column<int>(type: "int", nullable: false),
                    Rule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrammarTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrammarTask_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReadTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    TextReadingExample = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextReadingVoiceExample = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionsData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FixString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FixStringCorrect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadTask_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WriteTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Rule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FixString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FixStringCorrect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WriteTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WriteTask_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressAlfabet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scope = table.Column<int>(type: "int", nullable: false),
                    AlfabetTaskId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressAlfabet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressAlfabet_AlfabetTask_AlfabetTaskId",
                        column: x => x.AlfabetTaskId,
                        principalTable: "AlfabetTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgressAlfabet_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgresGrammar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scope = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GrammarTaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgresGrammar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgresGrammar_GrammarTask_GrammarTaskId",
                        column: x => x.GrammarTaskId,
                        principalTable: "GrammarTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgresGrammar_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgresRead",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scope = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadTaskId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgresRead", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgresRead_ReadTask_ReadTaskId",
                        column: x => x.ReadTaskId,
                        principalTable: "ReadTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgresRead_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgresWrite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WriteTaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgresWrite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgresWrite_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgresWrite_WriteTask_WriteTaskId",
                        column: x => x.WriteTaskId,
                        principalTable: "WriteTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordDictionary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transcription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Translation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WriteTaskId = table.Column<int>(type: "int", nullable: true),
                    AlfabetTaskId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordDictionary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordDictionary_AlfabetTask_AlfabetTaskId",
                        column: x => x.AlfabetTaskId,
                        principalTable: "AlfabetTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WordDictionary_WriteTask_WriteTaskId",
                        column: x => x.WriteTaskId,
                        principalTable: "WriteTask",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlfabetTask_SectionId",
                table: "AlfabetTask",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_GrammarTask_SectionId",
                table: "GrammarTask",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresGrammar_GrammarTaskId",
                table: "ProgresGrammar",
                column: "GrammarTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresGrammar_UserId",
                table: "ProgresGrammar",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresRead_ReadTaskId",
                table: "ProgresRead",
                column: "ReadTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresRead_UserId",
                table: "ProgresRead",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressAlfabet_AlfabetTaskId",
                table: "ProgressAlfabet",
                column: "AlfabetTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressAlfabet_UserId",
                table: "ProgressAlfabet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresWrite_UserId",
                table: "ProgresWrite",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresWrite_WriteTaskId",
                table: "ProgresWrite",
                column: "WriteTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadTask_SectionId",
                table: "ReadTask",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_WordDictionary_AlfabetTaskId",
                table: "WordDictionary",
                column: "AlfabetTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_WordDictionary_WriteTaskId",
                table: "WordDictionary",
                column: "WriteTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_WriteTask_SectionId",
                table: "WriteTask",
                column: "SectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgresGrammar");

            migrationBuilder.DropTable(
                name: "ProgresRead");

            migrationBuilder.DropTable(
                name: "ProgressAlfabet");

            migrationBuilder.DropTable(
                name: "ProgresWrite");

            migrationBuilder.DropTable(
                name: "WordDictionary");

            migrationBuilder.DropTable(
                name: "GrammarTask");

            migrationBuilder.DropTable(
                name: "ReadTask");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "AlfabetTask");

            migrationBuilder.DropTable(
                name: "WriteTask");

            migrationBuilder.DropTable(
                name: "Section");
        }
    }
}
