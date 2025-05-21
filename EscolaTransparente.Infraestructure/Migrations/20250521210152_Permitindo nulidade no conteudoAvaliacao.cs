using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaTransparente.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class PermitindonulidadenoconteudoAvaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ConteudoAvaliacao",
                table: "Avaliacao",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ConteudoAvaliacao",
                table: "Avaliacao",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
