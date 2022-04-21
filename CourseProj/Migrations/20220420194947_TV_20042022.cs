using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseProj.Migrations
{
    public partial class TV_20042022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

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
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    salt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unblocked = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userID = table.Column<int>(type: "int", nullable: false),
                    NumericField1_visible = table.Column<bool>(type: "bit", nullable: false),
                    NumericField1_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumericField2_visible = table.Column<bool>(type: "bit", nullable: false),
                    NumericField2_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumericField3_visible = table.Column<bool>(type: "bit", nullable: false),
                    NumericField3_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StringField1_visible = table.Column<bool>(type: "bit", nullable: false),
                    StringField1_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StringField2_visible = table.Column<bool>(type: "bit", nullable: false),
                    StringField2_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StringField3_visible = table.Column<bool>(type: "bit", nullable: false),
                    StringField3_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextField1_visible = table.Column<bool>(type: "bit", nullable: false),
                    TextField1_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextField2_visible = table.Column<bool>(type: "bit", nullable: false),
                    TextField2_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextField3_visible = table.Column<bool>(type: "bit", nullable: false),
                    TextField3_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateField1_visible = table.Column<bool>(type: "bit", nullable: false),
                    DateField1_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateField2_visible = table.Column<bool>(type: "bit", nullable: false),
                    DateField2_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateField3_visible = table.Column<bool>(type: "bit", nullable: false),
                    DateField3_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BooleanField1_visible = table.Column<bool>(type: "bit", nullable: false),
                    BooleanField1_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BooleanField2_visible = table.Column<bool>(type: "bit", nullable: false),
                    BooleanField2_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BooleanField3_visible = table.Column<bool>(type: "bit", nullable: false),
                    BooleanField3_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Collection_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Collection_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionID = table.Column<int>(type: "int", nullable: false),
                    NumericField1 = table.Column<int>(type: "int", nullable: false),
                    NumericField2 = table.Column<int>(type: "int", nullable: false),
                    NumericField3 = table.Column<int>(type: "int", nullable: false),
                    StringField1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StringField2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StringField3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextField1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextField2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextField3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateField1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateField2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateField3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BooleanField1 = table.Column<bool>(type: "bit", nullable: false),
                    BooleanField2 = table.Column<bool>(type: "bit", nullable: false),
                    BooleanField3 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Item_Collection_CollectionID",
                        column: x => x.CollectionID,
                        principalTable: "Collection",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comment_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ItemID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Like_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Like_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID", "Name" },
                values: new object[] { 2, "user" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "Email", "Password", "RoleId", "Unblocked", "salt" },
                values: new object[] { 69, "admin", "jna+GLxaSmyAWCGRZTmXQg==", 1, true, new byte[] { 197, 152, 153, 221, 33, 48, 0, 86 } });

            migrationBuilder.CreateIndex(
                name: "IX_Collection_ImageId",
                table: "Collection",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collection_userID",
                table: "Collection",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ItemID",
                table: "Comment",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserID",
                table: "Comment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CollectionID",
                table: "Item",
                column: "CollectionID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTag_tagsID",
                table: "ItemTag",
                column: "tagsID");

            migrationBuilder.CreateIndex(
                name: "IX_Like_ItemID",
                table: "Like",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserID",
                table: "Like",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "ItemTag");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
