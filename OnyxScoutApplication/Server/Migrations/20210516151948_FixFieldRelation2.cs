using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class FixFieldRelation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Fields_FieldId",
                table: "Fields");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Fields_FieldId",
                table: "Fields",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Fields_FieldId",
                table: "Fields");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Fields_FieldId",
                table: "Fields",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id");
        }
    }
}
