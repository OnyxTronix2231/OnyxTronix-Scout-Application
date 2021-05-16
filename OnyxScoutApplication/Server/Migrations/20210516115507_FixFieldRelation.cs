using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class FixFieldRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Fields_FieldId",
                table: "Fields");

            migrationBuilder.AlterColumn<int>(
                name: "FieldStageId",
                table: "Fields",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Fields_FieldId",
                table: "Fields",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Fields_FieldId",
                table: "Fields");

            migrationBuilder.AlterColumn<int>(
                name: "FieldStageId",
                table: "Fields",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Fields_FieldId",
                table: "Fields",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
