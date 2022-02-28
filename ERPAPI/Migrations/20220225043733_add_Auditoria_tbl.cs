using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class add_Auditoria_tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auditoria",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Usuario = table.Column<string>(maxLength: 100, nullable: true),
                    Entidad = table.Column<string>(maxLength: 200, nullable: true),
                    Llave = table.Column<string>(maxLength: 200, nullable: true),
                    Accion = table.Column<string>(maxLength: 10, nullable: true),
                    ValoresNuevos = table.Column<string>(maxLength: 2147483647, nullable: true),
                    ValoresAntiguos = table.Column<string>(maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditoria", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditoria");
        }
    }
}
