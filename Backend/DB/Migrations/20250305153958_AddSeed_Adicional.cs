using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class AddSeed_Adicional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TipoTrabajos",
                columns: new[] { "Id_Tipo_Trabajo_Ttr", "Descripcion_Ttr" },
                values: new object[] { 3, "Adicional" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TipoTrabajos",
                keyColumn: "Id_Tipo_Trabajo_Ttr",
                keyValue: 3);
        }
    }
}
