using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestaoLar.Api.Migrations
{
    /// <inheritdoc />
    public partial class MakeContatoEmergenciaNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContatoEmergencia",
                table: "Moradores",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContatoEmergencia",
                table: "Moradores",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
