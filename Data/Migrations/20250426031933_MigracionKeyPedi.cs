using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiAplicacionWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracionKeyPedi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Equipo",
                table: "Players",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Equipo",
                table: "Players");
        }
    }
}
