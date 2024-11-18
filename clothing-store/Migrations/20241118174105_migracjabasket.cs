using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clothing_store.Migrations
{
    /// <inheritdoc />
    public partial class migracjabasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_BasketId",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BasketId",
                table: "Accounts",
                column: "BasketId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_BasketId",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BasketId",
                table: "Accounts",
                column: "BasketId");
        }
    }
}
