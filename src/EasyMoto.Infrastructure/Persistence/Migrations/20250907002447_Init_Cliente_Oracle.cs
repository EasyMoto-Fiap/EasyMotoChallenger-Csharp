using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMoto.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init_Cliente_Oracle : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENTE");
        }
    }
}
