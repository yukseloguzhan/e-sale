using Microsoft.EntityFrameworkCore.Migrations;

namespace ESale.DataAccess.Migrations
{
    public partial class AddFavoryBoxTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoryBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoryBoxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavoryItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    FavoryBoxId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoryItem_FavoryBoxes_FavoryBoxId",
                        column: x => x.FavoryBoxId,
                        principalTable: "FavoryBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoryItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoryItem_FavoryBoxId",
                table: "FavoryItem",
                column: "FavoryBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoryItem_ProductId",
                table: "FavoryItem",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoryItem");

            migrationBuilder.DropTable(
                name: "FavoryBoxes");
        }
    }
}
