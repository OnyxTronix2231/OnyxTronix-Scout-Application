using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Data.Migrations
{
    public partial class AddScoutFormForamtModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScoutFormFormats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoutFormFormats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScoutFormForamtId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DefaultValue = table.Column<string>(nullable: true),
                    MyProperty = table.Column<int>(nullable: false),
                    FieldType = table.Column<int>(nullable: false),
                    MaxValue = table.Column<int>(nullable: false),
                    MinValue = table.Column<int>(nullable: false),
                    Required = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Field_ScoutFormFormats_ScoutFormForamtId",
                        column: x => x.ScoutFormForamtId,
                        principalTable: "ScoutFormFormats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Field_ScoutFormForamtId",
                table: "Field",
                column: "ScoutFormForamtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "ScoutFormFormats");
        }
    }
}
