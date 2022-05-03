using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseProj.Migrations
{
    public partial class TV02052022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemUser");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Password", "salt" },
                values: new object[] { "sMpdCzxGj6Ogenw1X87+Rw==", new byte[] { 254, 218, 220, 71, 147, 157, 92, 226 } });
        }
    }
}
