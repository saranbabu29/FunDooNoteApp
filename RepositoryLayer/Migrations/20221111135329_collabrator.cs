using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class collabrator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollabratorTable",
                columns: table => new
                {
                    CollabratorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollabratorMail = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NoteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabratorTable", x => x.CollabratorId);
                    table.ForeignKey(
                        name: "FK_CollabratorTable_noteTable_NoteId",
                        column: x => x.NoteId,
                        principalTable: "noteTable",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CollabratorTable_userTable_UserId",
                        column: x => x.UserId,
                        principalTable: "userTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabratorTable_NoteId",
                table: "CollabratorTable",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_CollabratorTable_UserId",
                table: "CollabratorTable",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollabratorTable");
        }
    }
}
