using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMoto.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Locacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LOCACAO",
                columns: table => new
                {
                    ID_LOCACAO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CLIENTE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MOTO_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    INICIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    FIM = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    VALOR_DIARIA = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: false),
                    VALOR_TOTAL = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: false),
                    STATUS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CRIADO_EM = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ENCERRADO_EM = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOCACAO", x => x.ID_LOCACAO);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LOCACAO");
        }
    }
}
