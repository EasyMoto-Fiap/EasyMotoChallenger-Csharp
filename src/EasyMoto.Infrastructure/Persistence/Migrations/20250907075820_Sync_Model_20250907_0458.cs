using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMoto.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Sync_Model_20250907_0458 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CRIADO_EM",
                table: "LOCACAO");

            migrationBuilder.DropColumn(
                name: "ENCERRADO_EM",
                table: "LOCACAO");

            migrationBuilder.DropColumn(
                name: "MOTO_ID",
                table: "LOCACAO");

            migrationBuilder.DropColumn(
                name: "STATUS",
                table: "LOCACAO");

            migrationBuilder.DropColumn(
                name: "VALOR_DIARIA",
                table: "LOCACAO");

            migrationBuilder.DropColumn(
                name: "VALOR_TOTAL",
                table: "LOCACAO");

            migrationBuilder.RenameColumn(
                name: "INICIO",
                table: "LOCACAO",
                newName: "DATA_INICIO");

            migrationBuilder.RenameColumn(
                name: "FIM",
                table: "LOCACAO",
                newName: "DATA_FIM");

            migrationBuilder.AddColumn<string>(
                name: "STATUS_LOCACAO",
                table: "LOCACAO",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STATUS_LOCACAO",
                table: "LOCACAO");

            migrationBuilder.RenameColumn(
                name: "DATA_INICIO",
                table: "LOCACAO",
                newName: "INICIO");

            migrationBuilder.RenameColumn(
                name: "DATA_FIM",
                table: "LOCACAO",
                newName: "FIM");

            migrationBuilder.AddColumn<DateTime>(
                name: "CRIADO_EM",
                table: "LOCACAO",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ENCERRADO_EM",
                table: "LOCACAO",
                type: "TIMESTAMP(7)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MOTO_ID",
                table: "LOCACAO",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "STATUS",
                table: "LOCACAO",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "VALOR_DIARIA",
                table: "LOCACAO",
                type: "DECIMAL(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VALOR_TOTAL",
                table: "LOCACAO",
                type: "DECIMAL(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
