using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoyalLifeSavings.Migrations
{
    /// <inheritdoc />
    public partial class removetokenfromdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VitalSourceAccessToken",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "FulfillmentAdded",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FulfillmentAdded",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "VitalSourceAccessToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
