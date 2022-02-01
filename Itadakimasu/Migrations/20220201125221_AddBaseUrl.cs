using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itadakimasu.Migrations
{
    public partial class AddBaseUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchAPI",
                table: "FoodImage",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchAPI",
                table: "FoodImage");
        }
    }
}
