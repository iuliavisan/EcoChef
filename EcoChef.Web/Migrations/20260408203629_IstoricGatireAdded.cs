using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoChef.Web.Migrations
{
    /// <inheritdoc />
    public partial class IstoricGatireAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IstoricGatire",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetetaId = table.Column<int>(type: "int", nullable: false),
                    NrPortii = table.Column<int>(type: "int", nullable: false),
                    DataGatirii = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CostTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IstoricGatire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IstoricGatire_Retete_RetetaId",
                        column: x => x.RetetaId,
                        principalTable: "Retete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IstoricGatire_RetetaId",
                table: "IstoricGatire",
                column: "RetetaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IstoricGatire");
        }
    }
}
