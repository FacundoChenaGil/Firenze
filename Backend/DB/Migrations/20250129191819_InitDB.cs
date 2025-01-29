using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoTrabajos",
                columns: table => new
                {
                    Id_Tipo_Trabajo_Ttr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion_Ttr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTrabajos", x => x.Id_Tipo_Trabajo_Ttr);
                });

            migrationBuilder.CreateTable(
                name: "Trabajos",
                columns: table => new
                {
                    Id_Trabajo_Tr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion_Tr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio_Tr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duracion_Tr = table.Column<TimeSpan>(type: "time", nullable: false),
                    Adicional_Tr = table.Column<bool>(type: "bit", nullable: false),
                    Id_Tipo_Trabajo_Tr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajos", x => x.Id_Trabajo_Tr);
                    table.ForeignKey(
                        name: "FK_Trabajos_TipoTrabajos_Id_Tipo_Trabajo_Tr",
                        column: x => x.Id_Tipo_Trabajo_Tr,
                        principalTable: "TipoTrabajos",
                        principalColumn: "Id_Tipo_Trabajo_Ttr",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trabajos_Id_Tipo_Trabajo_Tr",
                table: "Trabajos",
                column: "Id_Tipo_Trabajo_Tr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trabajos");

            migrationBuilder.DropTable(
                name: "TipoTrabajos");
        }
    }
}
