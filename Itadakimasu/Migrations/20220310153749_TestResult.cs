using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itadakimasu.Migrations
{
    public partial class TestResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestResult",
                table: "FoodImage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestResult",
                table: "FoodImage");
        }
    }
}
