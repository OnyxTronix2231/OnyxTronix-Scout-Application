using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class AddOptionIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Index",
                table: "Option",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "Option");
        }
    }
}
