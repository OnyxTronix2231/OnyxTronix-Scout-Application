using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class AddOptionSelectField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoutFormData_ScoutForms_ScoutFormId",
                table: "ScoutFormData");

            migrationBuilder.AlterColumn<int>(
                name: "ScoutFormId",
                table: "ScoutFormData",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Options",
                table: "Field",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScoutFormData_ScoutForms_ScoutFormId",
                table: "ScoutFormData",
                column: "ScoutFormId",
                principalTable: "ScoutForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoutFormData_ScoutForms_ScoutFormId",
                table: "ScoutFormData");

            migrationBuilder.DropColumn(
                name: "Options",
                table: "Field");

            migrationBuilder.AlterColumn<int>(
                name: "ScoutFormId",
                table: "ScoutFormData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ScoutFormData_ScoutForms_ScoutFormId",
                table: "ScoutFormData",
                column: "ScoutFormId",
                principalTable: "ScoutForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
