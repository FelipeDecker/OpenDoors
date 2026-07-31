using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestaoLar.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveHistoricoAcolhimentoFromMorador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HistoricoAcolhimento",
                table: "Moradores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HistoricoAcolhimento",
                table: "Moradores",
                type: "TEXT",
                nullable: true);
        }
    }
}
