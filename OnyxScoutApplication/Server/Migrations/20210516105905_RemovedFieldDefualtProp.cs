using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class RemovedFieldDefualtProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoolDefaultValue",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "CascadeConditionDefaultValue",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "NumericDefaultValue",
                table: "Fields");

            migrationBuilder.RenameColumn(
                name: "TextDefaultValue",
                table: "Fields",
                newName: "DefaultValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefaultValue",
                table: "Fields",
                newName: "TextDefaultValue");

            migrationBuilder.AddColumn<bool>(
                name: "BoolDefaultValue",
                table: "Fields",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CascadeConditionDefaultValue",
                table: "Fields",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumericDefaultValue",
                table: "Fields",
                type: "int",
                nullable: true);
        }
    }
}
