using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class Add_Last_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadosTurno",
                columns: table => new
                {
                    Id_Estado_Turno_Et = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion_Et = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosTurno", x => x.Id_Estado_Turno_Et);
                });

            migrationBuilder.CreateTable(
                name: "TarjetasTrabajo",
                columns: table => new
                {
                    Id_Tarjeta_Trabajo_Tar = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Tar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion_Tar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagen_URL_Tar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Trabajo_Tar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarjetasTrabajo", x => x.Id_Tarjeta_Trabajo_Tar);
                    table.ForeignKey(
                        name: "FK_TarjetasTrabajo_Trabajos_Id_Trabajo_Tar",
                        column: x => x.Id_Trabajo_Tar,
                        principalTable: "Trabajos",
                        principalColumn: "Id_Trabajo_Tr",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiposUsuario",
                columns: table => new
                {
                    Id_Tipo_Usuario_Tus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion_Tus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposUsuario", x => x.Id_Tipo_Usuario_Tus);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id_Usuario_Us = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Usuario_Us = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraseña_Us = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre_Us = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido_Us = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono_Us = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo_Electronico_Us = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Tipo_Usuario_Us = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id_Usuario_Us);
                    table.ForeignKey(
                        name: "FK_Usuarios_TiposUsuario_Id_Tipo_Usuario_Us",
                        column: x => x.Id_Tipo_Usuario_Us,
                        principalTable: "TiposUsuario",
                        principalColumn: "Id_Tipo_Usuario_Tus",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    Id_Turno_Tu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha_Tu = table.Column<DateOnly>(type: "date", nullable: false),
                    Hora_Tu = table.Column<TimeOnly>(type: "time", nullable: false),
                    Seña_Tu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duracion_Tu = table.Column<TimeSpan>(type: "time", nullable: false),
                    Precio_Total_Tu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Id_Usuario_Tu = table.Column<int>(type: "int", nullable: false),
                    Id_Estado_Turno_Tu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.Id_Turno_Tu);
                    table.ForeignKey(
                        name: "FK_Turnos_EstadosTurno_Id_Estado_Turno_Tu",
                        column: x => x.Id_Estado_Turno_Tu,
                        principalTable: "EstadosTurno",
                        principalColumn: "Id_Estado_Turno_Et",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turnos_Usuarios_Id_Usuario_Tu",
                        column: x => x.Id_Usuario_Tu,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario_Us",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrabajosXTurnos",
                columns: table => new
                {
                    Id_Turno_Tt = table.Column<int>(type: "int", nullable: false),
                    Id_Trabajo_Tt = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrabajosXTurnos", x => new { x.Id_Turno_Tt, x.Id_Trabajo_Tt });
                    table.ForeignKey(
                        name: "FK_TrabajosXTurnos_Trabajos_Id_Trabajo_Tt",
                        column: x => x.Id_Trabajo_Tt,
                        principalTable: "Trabajos",
                        principalColumn: "Id_Trabajo_Tr",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrabajosXTurnos_Turnos_Id_Turno_Tt",
                        column: x => x.Id_Turno_Tt,
                        principalTable: "Turnos",
                        principalColumn: "Id_Turno_Tu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TarjetasTrabajo_Id_Trabajo_Tar",
                table: "TarjetasTrabajo",
                column: "Id_Trabajo_Tar");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajosXTurnos_Id_Trabajo_Tt",
                table: "TrabajosXTurnos",
                column: "Id_Trabajo_Tt");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_Id_Estado_Turno_Tu",
                table: "Turnos",
                column: "Id_Estado_Turno_Tu");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_Id_Usuario_Tu",
                table: "Turnos",
                column: "Id_Usuario_Tu");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Id_Tipo_Usuario_Us",
                table: "Usuarios",
                column: "Id_Tipo_Usuario_Us");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TarjetasTrabajo");

            migrationBuilder.DropTable(
                name: "TrabajosXTurnos");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "EstadosTurno");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "TiposUsuario");
        }
    }
}
