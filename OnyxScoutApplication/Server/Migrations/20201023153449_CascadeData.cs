using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class CascadeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScoutFormDataId",
                table: "ScoutFormData",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScoutFormData_ScoutFormDataId",
                table: "ScoutFormData",
                column: "ScoutFormDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoutFormData_ScoutFormData_ScoutFormDataId",
                table: "ScoutFormData",
                column: "ScoutFormDataId",
                principalTable: "ScoutFormData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoutFormData_ScoutFormData_ScoutFormDataId",
                table: "ScoutFormData");

            migrationBuilder.DropIndex(
                name: "IX_ScoutFormData_ScoutFormDataId",
                table: "ScoutFormData");

            migrationBuilder.DropColumn(
                name: "ScoutFormDataId",
                table: "ScoutFormData");
        }
    }
}
