using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaTransparente.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Adicionandopropriedade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "CaracteristicasEscola",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "CaracteristicasEscola");
        }
    }
}
