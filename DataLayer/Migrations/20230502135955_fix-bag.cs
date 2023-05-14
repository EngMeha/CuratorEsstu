using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class fixbag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Scholarship",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "Student",
                newName: "Href");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Student",
                newName: "FIO");

            migrationBuilder.AddColumn<int>(
                name: "Well",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Well",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "Href",
                table: "Student",
                newName: "SecondName");

            migrationBuilder.RenameColumn(
                name: "FIO",
                table: "Student",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Scholarship",
                table: "Student",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
