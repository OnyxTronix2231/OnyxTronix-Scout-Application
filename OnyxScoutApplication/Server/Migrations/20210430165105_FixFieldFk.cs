using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class FixFieldFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Field_ScoutFormFormats_ScoutFormFormatId",
                table: "Field");

            migrationBuilder.AlterColumn<int>(
                name: "ScoutFormFormatId",
                table: "Field",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Field_ScoutFormFormats_ScoutFormFormatId",
                table: "Field",
                column: "ScoutFormFormatId",
                principalTable: "ScoutFormFormats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Field_ScoutFormFormats_ScoutFormFormatId",
                table: "Field");

            migrationBuilder.AlterColumn<int>(
                name: "ScoutFormFormatId",
                table: "Field",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Field_ScoutFormFormats_ScoutFormFormatId",
                table: "Field",
                column: "ScoutFormFormatId",
                principalTable: "ScoutFormFormats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
