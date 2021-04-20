using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class customEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomAlliance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomAlliance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomAlliances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlueId = table.Column<int>(nullable: true),
                    RedId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomAlliances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomAlliances_CustomAlliance_BlueId",
                        column: x => x.BlueId,
                        principalTable: "CustomAlliance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomAlliances_CustomAlliance_RedId",
                        column: x => x.RedId,
                        principalTable: "CustomAlliance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomTeam",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamNumber = table.Column<int>(nullable: false),
                    Nickname = table.Column<string>(nullable: true),
                    CustomAllianceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomTeam_CustomAlliance_CustomAllianceId",
                        column: x => x.CustomAllianceId,
                        principalTable: "CustomAlliance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomMatch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(nullable: true),
                    MatchNumber = table.Column<int>(nullable: false),
                    WinningAlliance = table.Column<string>(nullable: true),
                    AlliancesId = table.Column<int>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomMatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomMatch_CustomAlliances_AlliancesId",
                        column: x => x.AlliancesId,
                        principalTable: "CustomAlliances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomMatch_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomAlliances_BlueId",
                table: "CustomAlliances",
                column: "BlueId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomAlliances_RedId",
                table: "CustomAlliances",
                column: "RedId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomMatch_AlliancesId",
                table: "CustomMatch",
                column: "AlliancesId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomMatch_EventId",
                table: "CustomMatch",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomTeam_CustomAllianceId",
                table: "CustomTeam",
                column: "CustomAllianceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomMatch");

            migrationBuilder.DropTable(
                name: "CustomTeam");

            migrationBuilder.DropTable(
                name: "CustomAlliances");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "CustomAlliance");
        }
    }
}
