using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class FixFormDataFk2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoutFormDataId",
                table: "FormData");

            migrationBuilder.DropColumn(
                name: "ScoutFormId",
                table: "FormData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScoutFormDataId",
                table: "FormData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScoutFormId",
                table: "FormData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
