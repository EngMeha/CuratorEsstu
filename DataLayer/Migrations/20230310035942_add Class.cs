using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class addClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_AspNetUsers_UserId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "BasisOfLerning",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "BasisOfLerningId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Group",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Event",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "BasisOfLearning",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasisOfLearning", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_BasisOfLerningId",
                table: "Student",
                column: "BasisOfLerningId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_AspNetUsers_UserId",
                table: "Group",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_BasisOfLearning_BasisOfLerningId",
                table: "Student",
                column: "BasisOfLerningId",
                principalTable: "BasisOfLearning",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_AspNetUsers_UserId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_BasisOfLearning_BasisOfLerningId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "BasisOfLearning");

            migrationBuilder.DropIndex(
                name: "IX_Student_BasisOfLerningId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "BasisOfLerningId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Event");

            migrationBuilder.AddColumn<string>(
                name: "BasisOfLerning",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Group",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_AspNetUsers_UserId",
                table: "Group",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
