using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoChef.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitializareModulRetete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Retete",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructiuni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimpPreparare = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retete", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientRetete",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetetaId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    CantitateNecesara = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientRetete", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientRetete_Ingrediente_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingrediente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientRetete_Retete_RetetaId",
                        column: x => x.RetetaId,
                        principalTable: "Retete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRetete_IngredientId",
                table: "IngredientRetete",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRetete_RetetaId",
                table: "IngredientRetete",
                column: "RetetaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientRetete");

            migrationBuilder.DropTable(
                name: "Retete");
        }
    }
}
