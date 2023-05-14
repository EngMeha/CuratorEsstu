using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class fixdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_FormOfStudy_FormOfStudyId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Group_GroupId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "FormOfStudy");

            migrationBuilder.DropIndex(
                name: "IX_Group_FormOfStudyId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "FormOfStudyId",
                table: "Group");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_GroupDirectory_GroupId",
                table: "Student",
                column: "GroupId",
                principalTable: "GroupDirectory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_GroupDirectory_GroupId",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "FormOfStudyId",
                table: "Group",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FormOfStudy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormOfStudy", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Group_FormOfStudyId",
                table: "Group",
                column: "FormOfStudyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_FormOfStudy_FormOfStudyId",
                table: "Group",
                column: "FormOfStudyId",
                principalTable: "FormOfStudy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Group_GroupId",
                table: "Student",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
