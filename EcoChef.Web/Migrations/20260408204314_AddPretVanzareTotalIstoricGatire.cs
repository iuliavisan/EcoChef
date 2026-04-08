using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoChef.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddPretVanzareTotalIstoricGatire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PretVanzareTotal",
                table: "IstoricGatire",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PretVanzareTotal",
                table: "IstoricGatire");
        }
    }
}
