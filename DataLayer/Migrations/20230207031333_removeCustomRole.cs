using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class removeCustomRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_ListRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ListRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ListRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListRoleId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ListRoleId",
                table: "AspNetUsers",
                column: "ListRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_ListRoleId",
                table: "AspNetUsers",
                column: "ListRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }
    }
}
