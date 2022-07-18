using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItadakimasuWeb.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlobName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BlobUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlobWidth = table.Column<int>(type: "int", nullable: false),
                    BlobHeight = table.Column<int>(type: "int", nullable: false),
                    BlobSize = table.Column<long>(type: "bigint", nullable: false),
                    BlobSName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BlobSUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlobSWidth = table.Column<int>(type: "int", nullable: false),
                    BlobSHeight = table.Column<int>(type: "int", nullable: false),
                    BlobSSize = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodImage", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "FoodImage");
        }
    }
}
