using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class change_group_with_adding_the_new_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormOfStudy",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "GraduationDepartment",
                table: "Group");

            migrationBuilder.AddColumn<int>(
                name: "CraduationDepartamentId",
                table: "Group",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FormOfStudyId",
                table: "Group",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CraduationDepartament",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CraduationDepartament", x => x.Id);
                });

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
                name: "IX_Group_CraduationDepartamentId",
                table: "Group",
                column: "CraduationDepartamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_FormOfStudyId",
                table: "Group",
                column: "FormOfStudyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_CraduationDepartament_CraduationDepartamentId",
                table: "Group",
                column: "CraduationDepartamentId",
                principalTable: "CraduationDepartament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_FormOfStudy_FormOfStudyId",
                table: "Group",
                column: "FormOfStudyId",
                principalTable: "FormOfStudy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_CraduationDepartament_CraduationDepartamentId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_FormOfStudy_FormOfStudyId",
                table: "Group");

            migrationBuilder.DropTable(
                name: "CraduationDepartament");

            migrationBuilder.DropTable(
                name: "FormOfStudy");

            migrationBuilder.DropIndex(
                name: "IX_Group_CraduationDepartamentId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_FormOfStudyId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "CraduationDepartamentId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "FormOfStudyId",
                table: "Group");

            migrationBuilder.AddColumn<string>(
                name: "FormOfStudy",
                table: "Group",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GraduationDepartment",
                table: "Group",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
