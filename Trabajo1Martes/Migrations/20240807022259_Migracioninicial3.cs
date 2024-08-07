using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabajo1.Migrations
{
    /// <inheritdoc />
    public partial class Migracioninicial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Intentos",
                table: "Usuarios",
                type: "int",
                unicode: false,
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "estado",
                table: "Usuarios",
                type: "bit",
                unicode: false,
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Intentos",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "Usuarios");
        }
    }
}
