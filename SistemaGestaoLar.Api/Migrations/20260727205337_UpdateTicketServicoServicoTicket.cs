using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SistemaGestaoLar.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTicketServicoServicoTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ServicosStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Justificativa",
                table: "TicketServicos");

            migrationBuilder.DropColumn(
                name: "ServicoTicket",
                table: "TicketServicos");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TicketServicos",
                newName: "ServicoTicketId");

            migrationBuilder.CreateTable(
                name: "ServicosTicket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicosTicket", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "ServicosStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Pendente");

            migrationBuilder.UpdateData(
                table: "ServicosStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Realizado");

            migrationBuilder.InsertData(
                table: "ServicosStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "NaoRealizado" });

            migrationBuilder.InsertData(
                table: "ServicosTicket",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Jantar" },
                    { 3, "Banho" },
                    { 4, "TrocaRoupas" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketServicos_ServicoTicketId",
                table: "TicketServicos",
                column: "ServicoTicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketServicos_ServicosTicket_ServicoTicketId",
                table: "TicketServicos",
                column: "ServicoTicketId",
                principalTable: "ServicosTicket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketServicos_ServicosTicket_ServicoTicketId",
                table: "TicketServicos");

            migrationBuilder.DropTable(
                name: "ServicosTicket");

            migrationBuilder.DropIndex(
                name: "IX_TicketServicos_ServicoTicketId",
                table: "TicketServicos");

            migrationBuilder.DeleteData(
                table: "ServicosStatus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "ServicoTicketId",
                table: "TicketServicos",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Justificativa",
                table: "TicketServicos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServicoTicket",
                table: "TicketServicos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.InsertData(
                table: "ServicosStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Pendente" });
        }
    }
}
