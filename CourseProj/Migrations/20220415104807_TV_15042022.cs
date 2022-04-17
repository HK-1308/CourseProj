using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseProj.Migrations
{
    public partial class TV_15042022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 69,
                columns: new[] { "Password", "salt" },
                values: new object[] { "BizBOvJul/d+G6iL5qAd2A==", new byte[] { 204, 37, 75, 233, 20, 8, 145, 225 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 69,
                columns: new[] { "Password", "salt" },
                values: new object[] { "LdkOetqPnCn8mflQJGFwMA==", new byte[] { 64, 95, 46, 173, 165, 185, 179, 3 } });
        }
    }
}
