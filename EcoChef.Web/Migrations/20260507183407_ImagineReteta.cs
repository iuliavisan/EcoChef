using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoChef.Web.Migrations
{
    /// <inheritdoc />
    public partial class ImagineReteta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagineReteta",
                table: "Retete",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagineReteta",
                table: "Retete");
        }
    }
}
