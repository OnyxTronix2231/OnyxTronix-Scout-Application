using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class FixFormDataFk3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormData_FormDataInStage_FormDataInStageId",
                table: "FormData");

            migrationBuilder.DropIndex(
                name: "IX_FormData_FormDataInStageId",
                table: "FormData");

            migrationBuilder.DropColumn(
                name: "ScoutFormId",
                table: "FormDataInStage");

            migrationBuilder.DropColumn(
                name: "FormDataInStageId",
                table: "FormData");

            migrationBuilder.CreateIndex(
                name: "IX_FormData_FormDataStageId",
                table: "FormData",
                column: "FormDataStageId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormData_FormDataInStage_FormDataStageId",
                table: "FormData",
                column: "FormDataStageId",
                principalTable: "FormDataInStage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormData_FormDataInStage_FormDataStageId",
                table: "FormData");

            migrationBuilder.DropIndex(
                name: "IX_FormData_FormDataStageId",
                table: "FormData");

            migrationBuilder.AddColumn<int>(
                name: "ScoutFormId",
                table: "FormDataInStage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormDataInStageId",
                table: "FormData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormData_FormDataInStageId",
                table: "FormData",
                column: "FormDataInStageId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormData_FormDataInStage_FormDataInStageId",
                table: "FormData",
                column: "FormDataInStageId",
                principalTable: "FormDataInStage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
