using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class AddImageToScoutForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "ScoutForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "ScoutForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsImageUploaded",
                table: "ScoutForms",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "ScoutForms");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "ScoutForms");

            migrationBuilder.DropColumn(
                name: "IsImageUploaded",
                table: "ScoutForms");
        }
    }
}
