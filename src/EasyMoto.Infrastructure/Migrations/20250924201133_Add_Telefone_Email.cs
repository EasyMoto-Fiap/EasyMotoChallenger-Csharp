using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMoto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Telefone_Email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "clientes",
                type: "character varying(160)",
                maxLength: 160,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "telefone",
                table: "clientes",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "telefone",
                table: "clientes");
        }
    }
}
