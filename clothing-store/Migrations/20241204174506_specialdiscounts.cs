using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clothing_store.Migrations
{
    /// <inheritdoc />
    public partial class specialdiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDiscounts_Accounts_AccountId",
                table: "SpecialDiscounts");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "SpecialDiscounts");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "SpecialDiscounts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "SpecialDiscounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialDiscounts_Accounts_AccountId",
                table: "SpecialDiscounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDiscounts_Accounts_AccountId",
                table: "SpecialDiscounts");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "SpecialDiscounts");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "SpecialDiscounts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "SpecialDiscounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialDiscounts_Accounts_AccountId",
                table: "SpecialDiscounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId");
        }
    }
}
