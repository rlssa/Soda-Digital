using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoyalLifeSavings.Migrations
{
    /// <inheritdoc />
    public partial class fulfillmentcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VitalSourceFulfillmetCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VitalSourceFulfillmetCode",
                table: "AspNetUsers");
        }
    }
}
