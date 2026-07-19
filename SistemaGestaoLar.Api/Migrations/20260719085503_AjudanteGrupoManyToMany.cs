using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestaoLar.Api.Migrations
{
    /// <inheritdoc />
    public partial class AjudanteGrupoManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GruposAtribuidos",
                table: "Ajudantes");

            migrationBuilder.DropColumn(
                name: "Habilidades",
                table: "Ajudantes");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Ajudantes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Disponibilidade",
                table: "Ajudantes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

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

            migrationBuilder.CreateIndex(
                name: "IX_AjudanteGrupo_GruposId",
                table: "AjudanteGrupo",
                column: "GruposId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AjudanteGrupo");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Ajudantes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Disponibilidade",
                table: "Ajudantes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GruposAtribuidos",
                table: "Ajudantes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Habilidades",
                table: "Ajudantes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
