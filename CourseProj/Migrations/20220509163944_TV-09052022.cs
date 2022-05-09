using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseProj.Migrations
{
    public partial class TV09052022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Image");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Password", "salt" },
                values: new object[] { "TGiN2zjZu1wiwwz3cYplHg==", new byte[] { 139, 36, 234, 81, 20, 140, 245, 177 } });

            migrationBuilder.CreateIndex(
                name: "IX_Item_ImageId",
                table: "Item",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Image_ImageId",
                table: "Item",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Image_ImageId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ImageId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Image",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Password", "salt" },
                values: new object[] { "TQbYA3wLfp0zvLjey/GDIw==", new byte[] { 216, 7, 11, 139, 208, 101, 3, 14 } });
        }
    }
}
