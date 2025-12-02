using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagnoliaDB.Migrations
{
    /// <inheritdoc />
    public partial class AgregarModuloRenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PrendaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Costo = table.Column<decimal>(type: "TEXT", nullable: false),
                    FechaDeAlta = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Existencia = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventarios_Prendas_PrendaId",
                        column: x => x.PrendaId,
                        principalTable: "Prendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaRenta = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaDevolucion = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesRenta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RentaId = table.Column<int>(type: "INTEGER", nullable: false),
                    PrendaId = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecioRenta = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesRenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesRenta_Prendas_PrendaId",
                        column: x => x.PrendaId,
                        principalTable: "Prendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesRenta_Rentas_RentaId",
                        column: x => x.RentaId,
                        principalTable: "Rentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prendas_CategoriaId",
                table: "Prendas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesRenta_PrendaId",
                table: "DetallesRenta",
                column: "PrendaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesRenta_RentaId",
                table: "DetallesRenta",
                column: "RentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_PrendaId",
                table: "Inventarios",
                column: "PrendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentas_ClienteId",
                table: "Rentas",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prendas_Categorias_CategoriaId",
                table: "Prendas",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prendas_Categorias_CategoriaId",
                table: "Prendas");

            migrationBuilder.DropTable(
                name: "DetallesRenta");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "Rentas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Prendas_CategoriaId",
                table: "Prendas");
        }
    }
}
