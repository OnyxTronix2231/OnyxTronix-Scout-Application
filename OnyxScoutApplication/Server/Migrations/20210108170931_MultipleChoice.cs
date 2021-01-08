using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class MultipleChoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaximumSelectionLength",
                table: "Field",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumSelectionLength",
                table: "Field");
        }
    }
}
