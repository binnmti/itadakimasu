using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItadakimasuWeb.Migrations
{
    public partial class AddReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusReason",
                table: "FoodImage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusReason",
                table: "FoodImage");
        }
    }
}
