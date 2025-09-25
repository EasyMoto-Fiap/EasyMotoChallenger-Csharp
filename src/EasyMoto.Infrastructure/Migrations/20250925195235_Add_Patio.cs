using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMoto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Patio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    table.PrimaryKey("PK_funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_funcionarios_filiais_FilialId",
                        column: x => x.FilialId,
                        principalTable: "filiais",
                        principalColumn: "IdFilial",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "patios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomePatio = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    TamanhoPatio = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Andar = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    FilialId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_funcionarios_FilialId",
                table: "funcionarios",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_patios_FilialId",
                table: "patios",
                column: "FilialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "funcionarios");

            migrationBuilder.DropTable(
                name: "patios");
        }
    }
}
