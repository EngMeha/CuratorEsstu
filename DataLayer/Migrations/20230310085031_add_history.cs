using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class add_history : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Student",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "HistoryChangeStudent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    PercentOfAttedence = table.Column<int>(type: "int", nullable: false),
                    PercentOfProgress = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryChangeStudent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryChangeStudent_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryChangeStudent_StudentId",
                table: "HistoryChangeStudent",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryChangeStudent");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Student");
        }
    }
}
