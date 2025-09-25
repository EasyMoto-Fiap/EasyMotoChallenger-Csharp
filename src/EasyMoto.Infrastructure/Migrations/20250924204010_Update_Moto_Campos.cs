using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMoto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Moto_Campos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnoFabricacao",
                table: "motos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "LocacaoId",
                table: "motos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocalizacaoId",
                table: "motos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "motos",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_motos_Placa",
                table: "motos",
                column: "Placa",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_motos_Placa",
                table: "motos");

            migrationBuilder.DropColumn(
                name: "AnoFabricacao",
                table: "motos");

            migrationBuilder.DropColumn(
                name: "LocacaoId",
                table: "motos");

            migrationBuilder.DropColumn(
                name: "LocalizacaoId",
                table: "motos");

            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "motos");
        }
    }
}
