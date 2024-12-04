using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clothing_store.Migrations
{
    /// <inheritdoc />
    public partial class migracjarabat2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "SpecialDiscounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "SpecialDiscounts");
        }
    }
}
