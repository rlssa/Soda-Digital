using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoyalLifeSavings.Migrations
{
    /// <inheritdoc />
    public partial class addtaxrates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TaxApplicable",
                table: "EBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData("EBooks", "Id", "VCS0429073907956", "TaxApplicable", true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxApplicable",
                table: "EBooks");
        }
    }
}
