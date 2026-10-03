using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestaoLar.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTicketsOrfaosEAdicionaFkMorador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "DELETE FROM TicketServicos WHERE TicketDiarioId IN " +
                "(SELECT Id FROM TicketDiarios WHERE MoradorId NOT IN (SELECT Id FROM Moradores));");
            migrationBuilder.Sql(
                "DELETE FROM TicketDiarios WHERE MoradorId NOT IN (SELECT Id FROM Moradores);");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDiarios_MoradorId",
                table: "TicketDiarios",
                column: "MoradorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDiarios_Moradores_MoradorId",
                table: "TicketDiarios",
                column: "MoradorId",
                principalTable: "Moradores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketDiarios_Moradores_MoradorId",
                table: "TicketDiarios");

            migrationBuilder.DropIndex(
                name: "IX_TicketDiarios_MoradorId",
                table: "TicketDiarios");
        }
    }
}
