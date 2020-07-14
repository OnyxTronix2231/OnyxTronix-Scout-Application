using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Data.Migrations
{
    public partial class SeparateFieldDefValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultValue",
                table: "Field");

            migrationBuilder.AddColumn<bool>(
                name: "BoolDefaultValue",
                table: "Field",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumricDefaultValue",
                table: "Field",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextDefaultValue",
                table: "Field",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoolDefaultValue",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "NumricDefaultValue",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "TextDefaultValue",
                table: "Field");

            migrationBuilder.AddColumn<string>(
                name: "DefaultValue",
                table: "Field",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
