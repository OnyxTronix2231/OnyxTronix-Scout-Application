using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class FixFieldFk2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Field_FieldsInStage_FieldsInStageId",
                table: "Field");

            migrationBuilder.DropForeignKey(
                name: "FK_Field_ScoutFormFormats_ScoutFormFormatId",
                table: "Field");

            migrationBuilder.DropIndex(
                name: "IX_Field_FieldsInStageId",
                table: "Field");

            migrationBuilder.DropIndex(
                name: "IX_Field_ScoutFormFormatId",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "FieldsInStageId",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "ScoutFormFormatId",
                table: "Field");

            migrationBuilder.CreateIndex(
                name: "IX_Field_FieldStageId",
                table: "Field",
                column: "FieldStageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Field_FieldsInStage_FieldStageId",
                table: "Field",
                column: "FieldStageId",
                principalTable: "FieldsInStage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Field_FieldsInStage_FieldStageId",
                table: "Field");

            migrationBuilder.DropIndex(
                name: "IX_Field_FieldStageId",
                table: "Field");

            migrationBuilder.AddColumn<int>(
                name: "FieldsInStageId",
                table: "Field",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScoutFormFormatId",
                table: "Field",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Field_FieldsInStageId",
                table: "Field",
                column: "FieldsInStageId");

            migrationBuilder.CreateIndex(
                name: "IX_Field_ScoutFormFormatId",
                table: "Field",
                column: "ScoutFormFormatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Field_FieldsInStage_FieldsInStageId",
                table: "Field",
                column: "FieldsInStageId",
                principalTable: "FieldsInStage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Field_ScoutFormFormats_ScoutFormFormatId",
                table: "Field",
                column: "ScoutFormFormatId",
                principalTable: "ScoutFormFormats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
