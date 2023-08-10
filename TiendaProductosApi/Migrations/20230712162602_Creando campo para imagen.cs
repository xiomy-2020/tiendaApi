using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaProductosApi.Migrations
{
    /// <inheritdoc />
    public partial class Creandocampoparaimagen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenProducto",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenProducto",
                table: "Productos");
        }
    }
}
