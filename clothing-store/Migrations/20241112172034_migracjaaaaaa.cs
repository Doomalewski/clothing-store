using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clothing_store.Migrations
{
    /// <inheritdoc />
    public partial class migracjaaaaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingMethod_ShippingMethodId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingMethod",
                table: "ShippingMethod");

            migrationBuilder.RenameTable(
                name: "ShippingMethod",
                newName: "ShippingMethods");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingMethods",
                table: "ShippingMethods",
                column: "ShippingMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingMethods_ShippingMethodId",
                table: "Orders",
                column: "ShippingMethodId",
                principalTable: "ShippingMethods",
                principalColumn: "ShippingMethodId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingMethods_ShippingMethodId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingMethods",
                table: "ShippingMethods");

            migrationBuilder.RenameTable(
                name: "ShippingMethods",
                newName: "ShippingMethod");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingMethod",
                table: "ShippingMethod",
                column: "ShippingMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingMethod_ShippingMethodId",
                table: "Orders",
                column: "ShippingMethodId",
                principalTable: "ShippingMethod",
                principalColumn: "ShippingMethodId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
