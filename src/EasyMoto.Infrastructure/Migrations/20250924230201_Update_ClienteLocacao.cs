using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMoto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_ClienteLocacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_locacoes_clientes_ClienteId",
                table: "locacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_locacoes_motos_MotoId",
                table: "locacoes");

            migrationBuilder.DropIndex(
                name: "IX_locacoes_ClienteId",
                table: "locacoes");

            migrationBuilder.DropIndex(
                name: "IX_locacoes_MotoId",
                table: "locacoes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "inicio",
                table: "locacoes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fim",
                table: "locacoes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "locacoes",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Ativa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "locacoes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "inicio",
                table: "locacoes",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fim",
                table: "locacoes",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "IX_locacoes_ClienteId",
                table: "locacoes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_locacoes_MotoId",
                table: "locacoes",
                column: "MotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_locacoes_clientes_ClienteId",
                table: "locacoes",
                column: "ClienteId",
                principalTable: "clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_locacoes_motos_MotoId",
                table: "locacoes",
                column: "MotoId",
                principalTable: "motos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
