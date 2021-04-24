using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class ToDynamicStages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomMatch_CustomAlliances_AlliancesId",
                table: "CustomMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomMatch_Events_EventId",
                table: "CustomMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomTeam_CustomAlliance_CustomAllianceId",
                table: "CustomTeam");

            migrationBuilder.DropColumn(
                name: "FieldStageType",
                table: "Field");

            migrationBuilder.AddColumn<string>(
                name: "FieldStage",
                table: "Field",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomAllianceId",
                table: "CustomTeam",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "CustomMatch",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AlliancesId",
                table: "CustomMatch",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomMatch_CustomAlliances_AlliancesId",
                table: "CustomMatch",
                column: "AlliancesId",
                principalTable: "CustomAlliances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomMatch_Events_EventId",
                table: "CustomMatch",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomTeam_CustomAlliance_CustomAllianceId",
                table: "CustomTeam",
                column: "CustomAllianceId",
                principalTable: "CustomAlliance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomMatch_CustomAlliances_AlliancesId",
                table: "CustomMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomMatch_Events_EventId",
                table: "CustomMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomTeam_CustomAlliance_CustomAllianceId",
                table: "CustomTeam");

            migrationBuilder.DropColumn(
                name: "FieldStage",
                table: "Field");

            migrationBuilder.AddColumn<int>(
                name: "FieldStageType",
                table: "Field",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomAllianceId",
                table: "CustomTeam",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "CustomMatch",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AlliancesId",
                table: "CustomMatch",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CustomMatch_CustomAlliances_AlliancesId",
                table: "CustomMatch",
                column: "AlliancesId",
                principalTable: "CustomAlliances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomMatch_Events_EventId",
                table: "CustomMatch",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomTeam_CustomAlliance_CustomAllianceId",
                table: "CustomTeam",
                column: "CustomAllianceId",
                principalTable: "CustomAlliance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
