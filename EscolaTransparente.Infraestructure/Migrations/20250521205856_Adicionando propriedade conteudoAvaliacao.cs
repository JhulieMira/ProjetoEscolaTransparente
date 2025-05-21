using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaTransparente.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandopropriedadeconteudoAvaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConteudoAvaliacao",
                table: "Avaliacao",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConteudoAvaliacao",
                table: "Avaliacao");
        }
    }
}
