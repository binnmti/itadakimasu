using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItadakimasuWeb.Migrations
{
    public partial class ImageFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusNumber",
                table: "FoodImage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BlobSName",
                table: "Food",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusNumber",
                table: "FoodImage");

            migrationBuilder.DropColumn(
                name: "BlobSName",
                table: "Food");
        }
    }
}
