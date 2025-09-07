using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMoto.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Cliente_Contato_Required : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CPF_CLIENTE",
                table: "CLIENTE",
                type: "NVARCHAR2(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_CPF_CLIENTE",
                table: "CLIENTE",
                column: "CPF_CLIENTE",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_CPF_CLIENTE",
                table: "CLIENTE");

            migrationBuilder.AlterColumn<string>(
                name: "CPF_CLIENTE",
                table: "CLIENTE",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(450)");
        }
    }
}
