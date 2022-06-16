using Microsoft.EntityFrameworkCore.Migrations;

namespace SkateboardNeverDie.Infrastructure.Migrations
{
    public partial class InsertStances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Stances", new string[] { "Id", "Description" }, new string[] { "Goofy", "" });
            migrationBuilder.InsertData("Stances", new string[] { "Id", "Description" }, new string[] { "Regular", "" });
            migrationBuilder.InsertData("Stances", new string[] { "Id", "Description" }, new string[] { "Nollie", "" });
            migrationBuilder.InsertData("Stances", new string[] { "Id", "Description" }, new string[] { "Fakie", "" });
            migrationBuilder.InsertData("Stances", new string[] { "Id", "Description" }, new string[] { "Switch", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Stances", new string[] { "Id" }, new string[] { "Goofy" });
            migrationBuilder.DeleteData("Stances", new string[] { "Id" }, new string[] { "Regular" });
            migrationBuilder.DeleteData("Stances", new string[] { "Id" }, new string[] { "Nollie" });
            migrationBuilder.DeleteData("Stances", new string[] { "Id" }, new string[] { "Fakie" });
            migrationBuilder.DeleteData("Stances", new string[] { "Id" }, new string[] { "Switch" });
        }
    }
}
