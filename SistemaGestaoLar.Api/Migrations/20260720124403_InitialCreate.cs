using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SistemaGestaoLar.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ajudantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", nullable: false),
                    Disponibilidade = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ajudantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moradores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeCompleto = table.Column<string>(type: "TEXT", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ContatoEmergencia = table.Column<string>(type: "TEXT", nullable: true),
                    Observacoes = table.Column<string>(type: "TEXT", nullable: true),
                    HistoricoAcolhimento = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moradores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServicosStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicosStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketDiarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MoradorId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataServico = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketDiarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AjudanteGrupo",
                columns: table => new
                {
                    AjudantesId = table.Column<int>(type: "INTEGER", nullable: false),
                    GruposId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AjudanteGrupo", x => new { x.AjudantesId, x.GruposId });
                    table.ForeignKey(
                        name: "FK_AjudanteGrupo_Ajudantes_AjudantesId",
                        column: x => x.AjudantesId,
                        principalTable: "Ajudantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AjudanteGrupo_Grupos_GruposId",
                        column: x => x.GruposId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketServicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TicketDiarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    ServicoStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Justificativa = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketServicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketServicos_ServicosStatus_ServicoStatusId",
                        column: x => x.ServicoStatusId,
                        principalTable: "ServicosStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketServicos_TicketDiarios_TicketDiarioId",
                        column: x => x.TicketDiarioId,
                        principalTable: "TicketDiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ServicosStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Banho" },
                    { 2, "Troca de Roupa" },
                    { 3, "Jantar" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AjudanteGrupo_GruposId",
                table: "AjudanteGrupo",
                column: "GruposId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketServicos_ServicoStatusId",
                table: "TicketServicos",
                column: "ServicoStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketServicos_TicketDiarioId",
                table: "TicketServicos",
                column: "TicketDiarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AjudanteGrupo");

            migrationBuilder.DropTable(
                name: "Moradores");

            migrationBuilder.DropTable(
                name: "TicketServicos");

            migrationBuilder.DropTable(
                name: "Ajudantes");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "ServicosStatus");

            migrationBuilder.DropTable(
                name: "TicketDiarios");
        }
    }
}
