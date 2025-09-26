using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EasyMoto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ajuste_Tipo_FK_Operador_Filial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_filiais_empresas_EmpresaId",
                table: "filiais");

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
                    table.PrimaryKey("PK_operador", x => x.id_operador);
                    table.ForeignKey(
                        name: "FK_operador_filiais_filial_id",
                        column: x => x.filial_id,
                        principalTable: "filiais",
                        principalColumn: "IdFilial",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_operador_cpf_opr",
                table: "operador",
                column: "cpf_opr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_operador_filial_id",
                table: "operador",
                column: "filial_id");

            migrationBuilder.AddForeignKey(
                name: "FK_filiais_empresas_EmpresaId",
                table: "filiais",
                column: "EmpresaId",
                principalTable: "empresas",
                principalColumn: "IdEmpresa",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_filiais_empresas_EmpresaId",
                table: "filiais");

            migrationBuilder.DropTable(
                name: "operador");

            migrationBuilder.AddForeignKey(
                name: "FK_filiais_empresas_EmpresaId",
                table: "filiais",
                column: "EmpresaId",
                principalTable: "empresas",
                principalColumn: "IdEmpresa",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
