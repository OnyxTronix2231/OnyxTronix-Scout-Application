using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Data.Migrations
{
    public partial class AddedScoutForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FieldStageType",
                table: "Field",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ScoutForms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamNumber = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    MatchName = table.Column<string>(nullable: true),
                    WriterUserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoutForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScoutFormData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: true),
                    FieldID = table.Column<int>(nullable: false),
                    ScoutFormId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoutFormData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoutFormData_Field_FieldID",
                        column: x => x.FieldID,
                        principalTable: "Field",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoutFormData_ScoutForms_ScoutFormId",
                        column: x => x.ScoutFormId,
                        principalTable: "ScoutForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScoutFormData_FieldID",
                table: "ScoutFormData",
                column: "FieldID");

            migrationBuilder.CreateIndex(
                name: "IX_ScoutFormData_ScoutFormId",
                table: "ScoutFormData",
                column: "ScoutFormId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoutFormData");

            migrationBuilder.DropTable(
                name: "ScoutForms");

            migrationBuilder.AlterColumn<int>(
                name: "FieldStageType",
                table: "Field",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
