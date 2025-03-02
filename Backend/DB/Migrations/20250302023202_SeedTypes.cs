using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class SeedTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EstadosTurno",
                columns: new[] { "Id_Estado_Turno_Et", "Descripcion_Et" },
                values: new object[,]
                {
                    { 1, "Ausente" },
                    { 2, "Cancelado" },
                    { 3, "Pendiente de Pago" },
                    { 4, "Solicitado" },
                    { 5, "Finalizado" }
                });

            migrationBuilder.InsertData(
                table: "TipoTrabajos",
                columns: new[] { "Id_Tipo_Trabajo_Ttr", "Descripcion_Ttr" },
                values: new object[,]
                {
                    { 1, "Pedicuría" },
                    { 2, "Manicuría" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EstadosTurno",
                keyColumn: "Id_Estado_Turno_Et",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EstadosTurno",
                keyColumn: "Id_Estado_Turno_Et",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EstadosTurno",
                keyColumn: "Id_Estado_Turno_Et",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EstadosTurno",
                keyColumn: "Id_Estado_Turno_Et",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EstadosTurno",
                keyColumn: "Id_Estado_Turno_Et",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TipoTrabajos",
                keyColumn: "Id_Tipo_Trabajo_Ttr",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoTrabajos",
                keyColumn: "Id_Tipo_Trabajo_Ttr",
                keyValue: 2);
        }
    }
}
