using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ChangeCierresContables_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CierreContableLinea");

            migrationBuilder.DropTable(
                name: "CierreContable");

            migrationBuilder.CreateTable(
                name: "BitacoraCierreContable",
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
                    table.PrimaryKey("PK_BitacoraCierreContable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BitacoraCierreProceso",
                columns: table => new
                {
                    IdProceso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdBitacoraCierre = table.Column<int>(nullable: false),
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
                    table.PrimaryKey("PK_BitacoraCierreProceso", x => x.IdProceso);
                    table.ForeignKey(
                        name: "FK_BitacoraCierreProceso_BitacoraCierreContable_IdBitacoraCierre",
                        column: x => x.IdBitacoraCierre,
                        principalTable: "BitacoraCierreContable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BitacoraCierreProceso_IdBitacoraCierre",
                table: "BitacoraCierreProceso",
                column: "IdBitacoraCierre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BitacoraCierreProceso");

            migrationBuilder.DropTable(
                name: "BitacoraCierreContable");

            migrationBuilder.CreateTable(
                name: "CierreContable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<string>(nullable: true),
                    EstatusId = table.Column<long>(nullable: false),
                    FechaCierre = table.Column<DateTime>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Mensaje = table.Column<string>(nullable: true),
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
                    Estatus = table.Column<string>(nullable: true),
                    FechaCierre = table.Column<DateTime>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    IdBitacoracierreContable = table.Column<int>(nullable: false),
                    PasoCierre = table.Column<int>(nullable: false),
                    Proceso = table.Column<string>(nullable: true),
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
    }
}
