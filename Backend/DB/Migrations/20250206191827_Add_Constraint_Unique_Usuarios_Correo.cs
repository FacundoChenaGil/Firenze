using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class Add_Constraint_Unique_Usuarios_Correo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Correo_Electronico_Us",
                table: "Usuarios");

            migrationBuilder.AddUniqueConstraint(
                name: "UK_Correo_Electronico_Us",
                table: "Usuarios",
                column: "Correo_Electronico_Us");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "UK_Correo_Electronico_Us",
                table: "Usuarios");

            migrationBuilder.CreateIndex(
                name: "IX_Correo_Electronico_Us",
                table: "Usuarios",
                column: "Correo_Electronico_Us",
                unique: true);
        }
    }
}
