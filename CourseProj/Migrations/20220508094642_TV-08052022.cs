using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseProj.Migrations
{
    public partial class TV08052022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemUser");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Password", "salt" },
                values: new object[] { "TQbYA3wLfp0zvLjey/GDIw==", new byte[] { 216, 7, 11, 139, 208, 101, 3, 14 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemUser",
                columns: table => new
                {
                    itemsFavoriteID = table.Column<int>(type: "int", nullable: false),
                    usersFavoriteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemUser", x => new { x.itemsFavoriteID, x.usersFavoriteID });
                    table.ForeignKey(
                        name: "FK_ItemUser_Item_itemsFavoriteID",
                        column: x => x.itemsFavoriteID,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemUser_User_usersFavoriteID",
                        column: x => x.usersFavoriteID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Password", "salt" },
                values: new object[] { "NN/fKylIfrSYU+1CkhPbaA==", new byte[] { 218, 100, 28, 91, 22, 251, 149, 242 } });

            migrationBuilder.CreateIndex(
                name: "IX_ItemUser_usersFavoriteID",
                table: "ItemUser",
                column: "usersFavoriteID");
        }
    }
}
