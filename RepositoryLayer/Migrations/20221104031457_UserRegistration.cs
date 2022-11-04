using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class UserRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userEntities",
                table: "userEntities");

            migrationBuilder.RenameTable(
                name: "userEntities",
                newName: "userTable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userTable",
                table: "userTable",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userTable",
                table: "userTable");

            migrationBuilder.RenameTable(
                name: "userTable",
                newName: "userEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userEntities",
                table: "userEntities",
                column: "UserId");
        }
    }
}
