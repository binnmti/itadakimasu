﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itadakimasu.Migrations
{
    public partial class RefectorFood2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FoodImageCount",
                table: "Food",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodImageCount",
                table: "Food");
        }
    }
}
