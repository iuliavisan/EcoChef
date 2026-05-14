using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoChef.Web.Migrations
{
    /// <inheritdoc />
    public partial class EliminaPretProductie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PretProductie",
                table: "Retete");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PretProductie",
                table: "Retete",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
