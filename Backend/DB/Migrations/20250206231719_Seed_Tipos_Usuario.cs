using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Tipos_Usuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TiposUsuario",
                columns: new[] { "Id_Tipo_Usuario_Tus", "Descripcion_Tus" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Cliente" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TiposUsuario",
                keyColumn: "Id_Tipo_Usuario_Tus",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TiposUsuario",
                keyColumn: "Id_Tipo_Usuario_Tus",
                keyValue: 2);
        }
    }
}
