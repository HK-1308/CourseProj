using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseProj.Migrations
{
    public partial class TV_20042022_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "ID",
                keyValue: 69);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "Email", "Password", "RoleId", "Unblocked", "salt" },
                values: new object[] { 1, "root", "sMpdCzxGj6Ogenw1X87+Rw==", 1, true, new byte[] { 254, 218, 220, 71, 147, 157, 92, 226 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "Email", "Password", "RoleId", "Unblocked", "salt" },
                values: new object[] { 69, "admin", "jna+GLxaSmyAWCGRZTmXQg==", 1, true, new byte[] { 197, 152, 153, 221, 33, 48, 0, 86 } });
        }
    }
}
