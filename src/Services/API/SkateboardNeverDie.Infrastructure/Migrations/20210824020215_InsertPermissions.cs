using Microsoft.EntityFrameworkCore.Migrations;

namespace SkateboardNeverDie.Infrastructure.Migrations
{
    public partial class InsertPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Permissions", new string[] { "Id", "Name", "Description" }, new object[] { "skaters:add", "Add", "With this permission, the user can add skaters." });
            migrationBuilder.InsertData("Permissions", new string[] { "Id", "Name", "Description" }, new object[] { "skaters:edit", "Edit", "With this permission, the user can edit skaters." });
            migrationBuilder.InsertData("Permissions", new string[] { "Id", "Name", "Description" }, new object[] { "skaters:remove", "Remove", "With this permission, the user can remove skaters." });

            migrationBuilder.InsertData("Permissions", new string[] { "Id", "Name", "Description" }, new object[] { "tricks:add", "Add", "With this permission, the user can add tricks." });
            migrationBuilder.InsertData("Permissions", new string[] { "Id", "Name", "Description" }, new object[] { "tricks:edit", "Edit", "With this permission, the user can edit tricks." });
            migrationBuilder.InsertData("Permissions", new string[] { "Id", "Name", "Description" }, new object[] { "tricks:remove", "Remove", "With this permission, the user can remove tricks." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Permissions", new string[] { "Id" }, new string[] { "skaters:add" });
            migrationBuilder.DeleteData("Permissions", new string[] { "Id" }, new string[] { "skaters:edit" });
            migrationBuilder.DeleteData("Permissions", new string[] { "Id" }, new string[] { "skaters:remove" });

            migrationBuilder.DeleteData("Permissions", new string[] { "Id" }, new string[] { "tricks:add" });
            migrationBuilder.DeleteData("Permissions", new string[] { "Id" }, new string[] { "tricks:edit" });
            migrationBuilder.DeleteData("Permissions", new string[] { "Id" }, new string[] { "tricks:remove" });
        }
    }
}
