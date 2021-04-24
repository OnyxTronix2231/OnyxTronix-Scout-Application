using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScoutApplication.Server.Migrations
{
    public partial class AddStageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldStage",
                table: "Field");

            migrationBuilder.AddColumn<int>(
                name: "FieldStageid",
                table: "Field",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Field_FieldStageid",
                table: "Field",
                column: "FieldStageid");

            migrationBuilder.AddForeignKey(
                name: "FK_Field_Stage_FieldStageid",
                table: "Field",
                column: "FieldStageid",
                principalTable: "Stage",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Field_Stage_FieldStageid",
                table: "Field");

            migrationBuilder.DropTable(
                name: "Stage");

            migrationBuilder.DropIndex(
                name: "IX_Field_FieldStageid",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "FieldStageid",
                table: "Field");

            migrationBuilder.AddColumn<string>(
                name: "FieldStage",
                table: "Field",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
