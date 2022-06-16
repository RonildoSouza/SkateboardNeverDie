using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkateboardNeverDie.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tricks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tricks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skaters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Nickname = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Birthdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NaturalStanceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skaters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skaters_Stances_NaturalStanceId",
                        column: x => x.NaturalStanceId,
                        principalTable: "Stances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkaterTricks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TrickId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SkaterId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkaterTricks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkaterTricks_Skaters_SkaterId",
                        column: x => x.SkaterId,
                        principalTable: "Skaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkaterTricks_Tricks_TrickId",
                        column: x => x.TrickId,
                        principalTable: "Tricks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkaterTrickVariations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StanceId = table.Column<int>(type: "INTEGER", nullable: false),
                    SkaterTrickId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkaterTrickVariations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkaterTrickVariations_SkaterTricks_SkaterTrickId",
                        column: x => x.SkaterTrickId,
                        principalTable: "SkaterTricks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkaterTrickVariations_Stances_StanceId",
                        column: x => x.StanceId,
                        principalTable: "Stances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skaters_NaturalStanceId",
                table: "Skaters",
                column: "NaturalStanceId");

            migrationBuilder.CreateIndex(
                name: "IX_SkaterTricks_SkaterId",
                table: "SkaterTricks",
                column: "SkaterId");

            migrationBuilder.CreateIndex(
                name: "IX_SkaterTricks_TrickId",
                table: "SkaterTricks",
                column: "TrickId");

            migrationBuilder.CreateIndex(
                name: "IX_SkaterTrickVariations_SkaterTrickId",
                table: "SkaterTrickVariations",
                column: "SkaterTrickId");

            migrationBuilder.CreateIndex(
                name: "IX_SkaterTrickVariations_StanceId",
                table: "SkaterTrickVariations",
                column: "StanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkaterTrickVariations");

            migrationBuilder.DropTable(
                name: "SkaterTricks");

            migrationBuilder.DropTable(
                name: "Skaters");

            migrationBuilder.DropTable(
                name: "Tricks");

            migrationBuilder.DropTable(
                name: "Stances");
        }
    }
}
