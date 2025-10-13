using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagnoliaDB.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnToTableCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstaDisponible",
                table: "Categorias",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaDisponible",
                table: "Categorias");
        }
    }
}
