using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class Add_UK_Nombre_Usuario_Us : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "UK_Nombre_Usuario_Us",
                table: "Usuarios",
                column: "Nombre_Usuario_Us");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "UK_Nombre_Usuario_Us",
                table: "Usuarios");
        }
    }
}
