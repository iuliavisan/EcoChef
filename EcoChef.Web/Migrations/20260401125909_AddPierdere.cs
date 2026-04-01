using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoChef.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddPierdere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pierderi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    CantitatePierdere = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MotivPierdere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPierdere = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PretPierdere = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pierderi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pierderi_Ingrediente_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingrediente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pierderi_IngredientId",
                table: "Pierderi",
                column: "IngredientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pierderi");
        }
    }
}
