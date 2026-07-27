using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestaoLar.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServicoStatusSeedAndJustificativa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServicoTicket",
                table: "TicketServicos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ServicosStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Pendente");

            migrationBuilder.UpdateData(
                table: "ServicosStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Realizado");

            migrationBuilder.UpdateData(
                table: "ServicosStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "NaoRealizado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServicoTicket",
                table: "TicketServicos");

            migrationBuilder.UpdateData(
                table: "ServicosStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Banho");

            migrationBuilder.UpdateData(
                table: "ServicosStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Troca de Roupa");

            migrationBuilder.UpdateData(
                table: "ServicosStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Jantar");
        }
    }
}
