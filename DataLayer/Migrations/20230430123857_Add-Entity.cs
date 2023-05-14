using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class AddEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Group");

            migrationBuilder.AddColumn<int>(
                name: "GroupDirectoryId",
                table: "Group",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Speciality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupDirectory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Href = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SpecialityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDirectory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupDirectory_Speciality_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Speciality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Group_GroupDirectoryId",
                table: "Group",
                column: "GroupDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupDirectory_SpecialityId",
                table: "GroupDirectory",
                column: "SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_GroupDirectory_GroupDirectoryId",
                table: "Group",
                column: "GroupDirectoryId",
                principalTable: "GroupDirectory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_GroupDirectory_GroupDirectoryId",
                table: "Group");

            migrationBuilder.DropTable(
                name: "GroupDirectory");

            migrationBuilder.DropTable(
                name: "Speciality");

            migrationBuilder.DropIndex(
                name: "IX_Group_GroupDirectoryId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "GroupDirectoryId",
                table: "Group");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Group",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
