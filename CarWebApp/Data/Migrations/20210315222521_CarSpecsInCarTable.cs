using Microsoft.EntityFrameworkCore.Migrations;

namespace CarWebApp.Data.Migrations
{
    public partial class CarSpecsInCarTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarSpecs",
                table: "Car",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarSpecs",
                table: "Car");
        }
    }
}
