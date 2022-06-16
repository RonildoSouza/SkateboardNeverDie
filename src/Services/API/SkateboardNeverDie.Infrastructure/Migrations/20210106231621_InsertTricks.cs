using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SkateboardNeverDie.Infrastructure.Migrations
{
    public partial class InsertTricks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Tricks", new string[] { "Id", "Name" }, new object[] { Guid.NewGuid(), "Ollie" });
            migrationBuilder.InsertData("Tricks", new string[] { "Id", "Name" }, new object[] { Guid.NewGuid(), "Flip" });
            migrationBuilder.InsertData("Tricks", new string[] { "Id", "Name" }, new object[] { Guid.NewGuid(), "Heelflip" });
            migrationBuilder.InsertData("Tricks", new string[] { "Id", "Name" }, new object[] { Guid.NewGuid(), "Hardflip" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Tricks", new string[] { "Name" }, new string[] { "Ollie" });
            migrationBuilder.DeleteData("Tricks", new string[] { "Name" }, new string[] { "Flip" });
            migrationBuilder.DeleteData("Tricks", new string[] { "Name" }, new string[] { "Heelflip" });
            migrationBuilder.DeleteData("Tricks", new string[] { "Name" }, new string[] { "Hardflip" });
        }
    }
}
