using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMoto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Vaga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vagas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NumeroVaga = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    StatusVaga = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    MotoId = table.Column<Guid>(type: "uuid", nullable: true),
                    PatioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vagas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vagas_motos_MotoId",
                        column: x => x.MotoId,
                        principalTable: "motos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_vagas_patios_PatioId",
                        column: x => x.PatioId,
                        principalTable: "patios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_vagas_MotoId",
                table: "vagas",
                column: "MotoId");

            migrationBuilder.CreateIndex(
                name: "IX_vagas_PatioId",
                table: "vagas",
                column: "PatioId");

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
                name: "vagas");
        }
    }
}
