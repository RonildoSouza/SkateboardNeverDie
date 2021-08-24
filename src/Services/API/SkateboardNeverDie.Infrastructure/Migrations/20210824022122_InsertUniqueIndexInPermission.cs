using Microsoft.EntityFrameworkCore.Migrations;

namespace SkateboardNeverDie.Infrastructure.Migrations
{
    public partial class InsertUniqueIndexInPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Id",
                table: "Permissions",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_Id",
                table: "Permissions");
        }
    }
}
