using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EasyMoto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome_cliente = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cpf_cliente = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    telefone_cliente = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    email_cliente = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clientes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "empresas",
                columns: table => new
                {
                    id_empresa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome_empresa = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    cnpj = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_empresas", x => x.id_empresa);
                });

            migrationBuilder.CreateTable(
                name: "locacoes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cliente_id = table.Column<Guid>(type: "uuid", nullable: false),
                    moto_id = table.Column<Guid>(type: "uuid", nullable: false),
                    inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Ativa")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locacoes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "localizacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    status_loc = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    data_hora = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    zona_virtual = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    latitude = table.Column<double>(type: "double precision", nullable: true),
                    longitude = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_localizacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "motos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Placa = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Marca = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Modelo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Cor = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    AnoFabricacao = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    LocacaoId = table.Column<int>(type: "integer", nullable: true),
                    LocalizacaoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_motos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "filiais",
                columns: table => new
                {
                    IdFilial = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeFilial = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Cidade = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Estado = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Pais = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Endereco = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    EmpresaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_filiais", x => x.IdFilial);
                    table.ForeignKey(
                        name: "fk_filiais_empresas_empresa_id",
                        column: x => x.EmpresaId,
                        principalTable: "empresas",
                        principalColumn: "id_empresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "funcionarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeFuncionario = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Cpf = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    FilialId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "fk_funcionarios_filiais_filial_id",
                        column: x => x.FilialId,
                        principalTable: "filiais",
                        principalColumn: "IdFilial",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "operador",
                columns: table => new
                {
                    id_operador = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome_opr = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cpf_opr = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    telefone_opr = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    email_opr = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    filial_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_operador", x => x.id_operador);
                    table.ForeignKey(
                        name: "fk_operador_filiais_filial_id",
                        column: x => x.filial_id,
                        principalTable: "filiais",
                        principalColumn: "IdFilial",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "patios",
                columns: table => new
                {
                    IdPatio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomePatio = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    TamanhoPatio = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Andar = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    FilialId = table.Column<int>(type: "integer", nullable: false),
                    filial_id_filial = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_patios", x => x.IdPatio);
                    table.ForeignKey(
                        name: "fk_patios_filiais_filial_id",
                        column: x => x.FilialId,
                        principalTable: "filiais",
                        principalColumn: "IdFilial",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_patios_filiais_filial_id_filial",
                        column: x => x.filial_id_filial,
                        principalTable: "filiais",
                        principalColumn: "IdFilial");
                });

            migrationBuilder.CreateTable(
                name: "vagas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatioId = table.Column<int>(type: "integer", nullable: false),
                    NumeroVaga = table.Column<int>(type: "integer", nullable: false),
                    Ocupada = table.Column<bool>(type: "boolean", nullable: false),
                    MotoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vagas", x => x.Id);
                    table.ForeignKey(
                        name: "fk_vagas_motos_moto_id",
                        column: x => x.MotoId,
                        principalTable: "motos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_vagas_patios_patio_id",
                        column: x => x.PatioId,
                        principalTable: "patios",
                        principalColumn: "IdPatio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_clientes_cpf_cliente",
                table: "clientes",
                column: "cpf_cliente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_empresas_cnpj",
                table: "empresas",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_filiais_empresa_id",
                table: "filiais",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_funcionarios_FilialId",
                table: "funcionarios",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_motos_Placa",
                table: "motos",
                column: "Placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_operador_cpf_opr",
                table: "operador",
                column: "cpf_opr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_operador_filial_id",
                table: "operador",
                column: "filial_id");

            migrationBuilder.CreateIndex(
                name: "ix_patios_filial_id_filial",
                table: "patios",
                column: "filial_id_filial");

            migrationBuilder.CreateIndex(
                name: "IX_patios_FilialId",
                table: "patios",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "ix_vagas_moto_id",
                table: "vagas",
                column: "MotoId");

            migrationBuilder.CreateIndex(
                name: "IX_vagas_PatioId_NumeroVaga",
                table: "vagas",
                columns: new[] { "PatioId", "NumeroVaga" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "funcionarios");

            migrationBuilder.DropTable(
                name: "locacoes");

            migrationBuilder.DropTable(
                name: "localizacoes");

            migrationBuilder.DropTable(
                name: "operador");

            migrationBuilder.DropTable(
                name: "vagas");

            migrationBuilder.DropTable(
                name: "motos");

            migrationBuilder.DropTable(
                name: "patios");

            migrationBuilder.DropTable(
                name: "filiais");

            migrationBuilder.DropTable(
                name: "empresas");
        }
    }
}
