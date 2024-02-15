using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoyalLifeSavings.Migrations
{
    /// <inheritdoc />
    public partial class vitalsourceaccesstoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VitalSourceAccessToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VitalSourceAccessToken",
                table: "AspNetUsers");
        }
    }
}
