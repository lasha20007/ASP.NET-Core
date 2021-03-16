using Microsoft.EntityFrameworkCore.Migrations;

namespace CarWebApp.Data.Migrations
{
    public partial class addphotoname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Car");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Car",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photoname",
                table: "Car",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Photoname",
                table: "Car");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Car",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
