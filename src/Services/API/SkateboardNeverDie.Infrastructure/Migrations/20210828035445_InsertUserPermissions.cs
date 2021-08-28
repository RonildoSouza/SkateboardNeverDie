using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SkateboardNeverDie.Infrastructure.Migrations
{
    public partial class InsertUserPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var userId = Guid.NewGuid();
            migrationBuilder.InsertData("Users", new string[] { "Id", "IdentityUserId", "Name", "Email" }, new object[] { userId, Guid.Parse("d853537d-9059-4904-86af-b3dedd17d215"), "Colaborador 01", "colaborador01@localhost.com" });

            migrationBuilder.InsertData("UserPermissions", new string[] { "Id", "UserId", "PermissionId" }, new object[] { Guid.NewGuid(), userId, "skaters:add" });
            migrationBuilder.InsertData("UserPermissions", new string[] { "Id", "UserId", "PermissionId" }, new object[] { Guid.NewGuid(), userId, "skaters:edit" });
            migrationBuilder.InsertData("UserPermissions", new string[] { "Id", "UserId", "PermissionId" }, new object[] { Guid.NewGuid(), userId, "tricks:add" });
            migrationBuilder.InsertData("UserPermissions", new string[] { "Id", "UserId", "PermissionId" }, new object[] { Guid.NewGuid(), userId, "tricks:edit" });
        }

        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
