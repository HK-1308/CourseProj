using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseProj.Migrations
{
    public partial class TV13042022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 69,
                columns: new[] { "Email", "Password", "salt" },
                values: new object[] { "admin", "LdkOetqPnCn8mflQJGFwMA==", new byte[] { 64, 95, 46, 173, 165, 185, 179, 3 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 69,
                columns: new[] { "Email", "Password", "salt" },
                values: new object[] { "admin@mail.ru", "123456", null });
        }
    }
}
