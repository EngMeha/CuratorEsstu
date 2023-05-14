using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class fixgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Well",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "Well",
                table: "Group",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Well",
                table: "Group");

            migrationBuilder.AddColumn<int>(
                name: "Well",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
