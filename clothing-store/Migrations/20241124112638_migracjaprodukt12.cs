using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clothing_store.Migrations
{
    /// <inheritdoc />
    public partial class migracjaprodukt12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Baskets_BasketId",
                table: "BasketProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Products_ProductId",
                table: "BasketProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketProduct",
                table: "BasketProduct");

            migrationBuilder.RenameTable(
                name: "BasketProduct",
                newName: "BasketProducts");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProduct_ProductId",
                table: "BasketProducts",
                newName: "IX_BasketProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProduct_BasketId",
                table: "BasketProducts",
                newName: "IX_BasketProducts_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketProducts",
                table: "BasketProducts",
                column: "BasketProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Baskets_BasketId",
                table: "BasketProducts",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "BasketId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Products_ProductId",
                table: "BasketProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Baskets_BasketId",
                table: "BasketProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Products_ProductId",
                table: "BasketProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketProducts",
                table: "BasketProducts");

            migrationBuilder.RenameTable(
                name: "BasketProducts",
                newName: "BasketProduct");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProducts_ProductId",
                table: "BasketProduct",
                newName: "IX_BasketProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProducts_BasketId",
                table: "BasketProduct",
                newName: "IX_BasketProduct_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketProduct",
                table: "BasketProduct",
                column: "BasketProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Baskets_BasketId",
                table: "BasketProduct",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "BasketId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Products_ProductId",
                table: "BasketProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
