using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Data.Migrations
{
    public partial class AddFieldStageTypeProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Field");

            migrationBuilder.AddColumn<int>(
                name: "FieldStageType",
                table: "Field",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldStageType",
                table: "Field");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Field",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
