using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMoto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ajustes_Finais_Vagas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_vagas_motos_moto_id",
                table: "vagas");

            migrationBuilder.DropIndex(
                name: "ix_vagas_moto_id",
                table: "vagas");

            migrationBuilder.RenameColumn(
                name: "Ocupada",
                table: "vagas",
                newName: "ocupada");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "vagas",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PatioId",
                table: "vagas",
                newName: "patio_id");

            migrationBuilder.RenameColumn(
                name: "NumeroVaga",
                table: "vagas",
                newName: "numero_vaga");

            migrationBuilder.RenameColumn(
                name: "MotoId",
                table: "vagas",
                newName: "moto_id");

            migrationBuilder.RenameIndex(
                name: "IX_vagas_PatioId_NumeroVaga",
                table: "vagas",
                newName: "ix_vagas_patio_id_numero_vaga");

            migrationBuilder.AlterColumn<string>(
                name: "numero_vaga",
                table: "vagas",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ocupada",
                table: "vagas",
                newName: "Ocupada");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "vagas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "patio_id",
                table: "vagas",
                newName: "PatioId");

            migrationBuilder.RenameColumn(
                name: "numero_vaga",
                table: "vagas",
                newName: "NumeroVaga");

            migrationBuilder.RenameColumn(
                name: "moto_id",
                table: "vagas",
                newName: "MotoId");

            migrationBuilder.RenameIndex(
                name: "ix_vagas_patio_id_numero_vaga",
                table: "vagas",
                newName: "IX_vagas_PatioId_NumeroVaga");

            migrationBuilder.AlterColumn<int>(
                name: "NumeroVaga",
                table: "vagas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "ix_vagas_moto_id",
                table: "vagas",
                column: "MotoId");

            migrationBuilder.AddForeignKey(
                name: "fk_vagas_motos_moto_id",
                table: "vagas",
                column: "MotoId",
                principalTable: "motos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
