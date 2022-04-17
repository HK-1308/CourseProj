using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseProj.Migrations
{
    public partial class TV_020422 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ItemTag",
                columns: table => new
                {
                    itemsID = table.Column<int>(type: "int", nullable: false),
                    tagsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTag", x => new { x.itemsID, x.tagsID });
                    table.ForeignKey(
                        name: "FK_ItemTag_Item_itemsID",
                        column: x => x.itemsID,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemTag_Tag_tagsID",
                        column: x => x.tagsID,
                        principalTable: "Tag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 69,
                column: "Unblocked",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemTag_tagsID",
                table: "ItemTag",
                column: "tagsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 69,
                column: "Unblocked",
                value: false);
        }
    }
}
