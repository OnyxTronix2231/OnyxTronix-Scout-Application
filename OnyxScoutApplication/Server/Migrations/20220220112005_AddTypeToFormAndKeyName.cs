using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class AddTypeToFormAndKeyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.RenameColumn(
            //     name: "MatchName",
            //     table: "ScoutForms",
            //     newName: "KeyName");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ScoutForms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ScoutForms");

            // migrationBuilder.RenameColumn(
            //     name: "KeyName",
            //     table: "ScoutForms",
            //     newName: "MatchName");
        }
    }
}
