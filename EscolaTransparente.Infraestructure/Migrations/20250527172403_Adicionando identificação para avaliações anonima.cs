using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaTransparente.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Adicionandoidentificaçãoparaavaliaçõesanonima : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AvaliacaoAnonima",
                table: "Avaliacao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvaliacaoAnonima",
                table: "Avaliacao");
        }
    }
}
