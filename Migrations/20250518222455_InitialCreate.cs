using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMoto.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENTE",
                columns: table => new
                {
                    ID_CLIENTE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_CLIENTE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CPF_CLIENTE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TELEFONE_CLIENTE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL_CLIENTE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE", x => x.ID_CLIENTE);
                });

            migrationBuilder.CreateTable(
                name: "EMPRESA",
                columns: table => new
                {
                    ID_EMPRESA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_EMPRESA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CNPJ = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESA", x => x.ID_EMPRESA);
                });

            migrationBuilder.CreateTable(
                name: "LOCALIZACAO",
                columns: table => new
                {
                    ID_LOCALIZACAO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    STATUS_LOC = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DATA_HORA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    ZONA_VIRTUAL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    LATITUDE = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    LONGITUDE = table.Column<double>(type: "BINARY_DOUBLE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOCALIZACAO", x => x.ID_LOCALIZACAO);
                });

            migrationBuilder.CreateTable(
                name: "CLIENTE_LOCACAO",
                columns: table => new
                {
                    ID_LOCACAO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DATA_INICIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DATA_FIM = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    STATUS_LOCACAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CLIENTE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE_LOCACAO", x => x.ID_LOCACAO);
                    table.ForeignKey(
                        name: "FK_CLIENTE_LOCACAO_CLIENTE_CLIENTE_ID",
                        column: x => x.CLIENTE_ID,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FILIAL",
                columns: table => new
                {
                    ID_FILIAL = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_FILIAL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CIDADE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ESTADO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PAIS = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ENDERECO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMPRESA_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FILIAL", x => x.ID_FILIAL);
                    table.ForeignKey(
                        name: "FK_FILIAL_EMPRESA_EMPRESA_ID",
                        column: x => x.EMPRESA_ID,
                        principalTable: "EMPRESA",
                        principalColumn: "ID_EMPRESA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MOTO",
                columns: table => new
                {
                    ID_MOTO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PLACA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    MODELO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ANO_FABRICACAO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    STATUS_MOTO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    LOCACAO_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LOCALIZACAO_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOTO", x => x.ID_MOTO);
                    table.ForeignKey(
                        name: "FK_MOTO_CLIENTE_LOCACAO_LOCACAO_ID",
                        column: x => x.LOCACAO_ID,
                        principalTable: "CLIENTE_LOCACAO",
                        principalColumn: "ID_LOCACAO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MOTO_LOCALIZACAO_LOCALIZACAO_ID",
                        column: x => x.LOCALIZACAO_ID,
                        principalTable: "LOCALIZACAO",
                        principalColumn: "ID_LOCALIZACAO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FUNCIONARIO",
                columns: table => new
                {
                    ID_FUNC = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_FUNC = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CPF_FUNC = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CARGO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TELEFONE_FUNC = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL_FUNC = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    FILIAL_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNCIONARIO", x => x.ID_FUNC);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIO_FILIAL_FILIAL_ID",
                        column: x => x.FILIAL_ID,
                        principalTable: "FILIAL",
                        principalColumn: "ID_FILIAL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OPERADOR",
                columns: table => new
                {
                    ID_OPERADOR = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_OPR = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CPF_OPR = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TELEFONE_OPR = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL_OPR = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    FILIAL_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPERADOR", x => x.ID_OPERADOR);
                    table.ForeignKey(
                        name: "FK_OPERADOR_FILIAL_FILIAL_ID",
                        column: x => x.FILIAL_ID,
                        principalTable: "FILIAL",
                        principalColumn: "ID_FILIAL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PATIO",
                columns: table => new
                {
                    ID_PATIO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_PATIO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    TAMANHO_PATIO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ANDAR = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    FILIAL_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PATIO", x => x.ID_PATIO);
                    table.ForeignKey(
                        name: "FK_PATIO_FILIAL_FILIAL_ID",
                        column: x => x.FILIAL_ID,
                        principalTable: "FILIAL",
                        principalColumn: "ID_FILIAL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VAGA",
                columns: table => new
                {
                    ID_VAGA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    STATUS_VAGA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PATIO_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MOTO_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FILEIRA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    COLUNA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VAGA", x => x.ID_VAGA);
                    table.ForeignKey(
                        name: "FK_VAGA_MOTO_MOTO_ID",
                        column: x => x.MOTO_ID,
                        principalTable: "MOTO",
                        principalColumn: "ID_MOTO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VAGA_PATIO_PATIO_ID",
                        column: x => x.PATIO_ID,
                        principalTable: "PATIO",
                        principalColumn: "ID_PATIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_LOCACAO_CLIENTE_ID",
                table: "CLIENTE_LOCACAO",
                column: "CLIENTE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FILIAL_EMPRESA_ID",
                table: "FILIAL",
                column: "EMPRESA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_FILIAL_ID",
                table: "FUNCIONARIO",
                column: "FILIAL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_LOCACAO_ID",
                table: "MOTO",
                column: "LOCACAO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_LOCALIZACAO_ID",
                table: "MOTO",
                column: "LOCALIZACAO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OPERADOR_FILIAL_ID",
                table: "OPERADOR",
                column: "FILIAL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PATIO_FILIAL_ID",
                table: "PATIO",
                column: "FILIAL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VAGA_MOTO_ID",
                table: "VAGA",
                column: "MOTO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VAGA_PATIO_ID",
                table: "VAGA",
                column: "PATIO_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FUNCIONARIO");

            migrationBuilder.DropTable(
                name: "OPERADOR");

            migrationBuilder.DropTable(
                name: "VAGA");

            migrationBuilder.DropTable(
                name: "MOTO");

            migrationBuilder.DropTable(
                name: "PATIO");

            migrationBuilder.DropTable(
                name: "CLIENTE_LOCACAO");

            migrationBuilder.DropTable(
                name: "LOCALIZACAO");

            migrationBuilder.DropTable(
                name: "FILIAL");

            migrationBuilder.DropTable(
                name: "CLIENTE");

            migrationBuilder.DropTable(
                name: "EMPRESA");
        }
    }
}
