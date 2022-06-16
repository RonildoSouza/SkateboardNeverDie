using Microsoft.EntityFrameworkCore.Migrations;

namespace SkateboardNeverDie.Infrastructure.Migrations
{
    public partial class AddUniqueIndexTableTricks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tricks_Id_Name",
                table: "Tricks",
                columns: new[] { "Id", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tricks_Id_Name",
                table: "Tricks");
        }
    }
}
