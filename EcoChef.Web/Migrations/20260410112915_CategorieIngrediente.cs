using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoChef.Web.Migrations
{
    /// <inheritdoc />
    public partial class CategorieIngrediente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categorie",
                table: "Ingrediente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categorie",
                table: "Ingrediente");
        }
    }
}
