using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CierreContable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CierreContable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaCierre = table.Column<DateTime>(nullable: false),
                    EstatusId = table.Column<long>(nullable: false),
                    Estatus = table.Column<string>(nullable: true),
                    Mensaje = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CierreContable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CierreContableLinea",
                columns: table => new
                {
                    IdLinea = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdBitacoracierreContable = table.Column<int>(nullable: false),
                    FechaCierre = table.Column<DateTime>(nullable: false),
                    PasoCierre = table.Column<int>(nullable: false),
                    Proceso = table.Column<string>(nullable: true),
                    Estatus = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CierreContableLinea", x => x.IdLinea);
                    table.ForeignKey(
                        name: "FK_CierreContableLinea_CierreContable_IdBitacoracierreContable",
                        column: x => x.IdBitacoracierreContable,
                        principalTable: "CierreContable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CierreContableLinea_IdBitacoracierreContable",
                table: "CierreContableLinea",
                column: "IdBitacoracierreContable");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CierreContableLinea");

            migrationBuilder.DropTable(
                name: "CierreContable");
        }
    }
}
