using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clothing_store.Migrations
{
    /// <inheritdoc />
    public partial class migraccjajacjacj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Baskets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Baskets");
        }
    }
}
