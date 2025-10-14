using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagnoliaDB.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToPrendas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Prendas_CodigoDeReferencia",
                table: "Prendas",
                column: "CodigoDeReferencia",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prendas_CodigoDeReferencia",
                table: "Prendas");
        }
    }
}
