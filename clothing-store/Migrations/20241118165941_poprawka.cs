using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clothing_store.Migrations
{
    /// <inheritdoc />
    public partial class poprawka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Currencies_PreferredCurrencyCurrencyId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_PreferredCurrencyCurrencyId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PreferredCurrencyCurrencyId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Symbol",
                table: "Currencies",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "PlnToCurrRatio",
                table: "Currencies",
                newName: "Rate");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Currencies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Currencies");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "Currencies",
                newName: "PlnToCurrRatio");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Currencies",
                newName: "Symbol");

            migrationBuilder.AddColumn<int>(
                name: "PreferredCurrencyCurrencyId",
                table: "Accounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PreferredCurrencyCurrencyId",
                table: "Accounts",
                column: "PreferredCurrencyCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Currencies_PreferredCurrencyCurrencyId",
                table: "Accounts",
                column: "PreferredCurrencyCurrencyId",
                principalTable: "Currencies",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
