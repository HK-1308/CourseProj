using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseProj.Migrations
{
    public partial class TV04042022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "salt",
                table: "User",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "salt",
                table: "User");
        }
    }
}
