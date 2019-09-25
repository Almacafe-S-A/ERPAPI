using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Conciliacion_ConciliacionLinea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conciliacion",
                columns: table => new
                {
                    ConciliacionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdBanco = table.Column<long>(nullable: true),
                    BankName = table.Column<string>(nullable: false),
                    FechaConciliacion = table.Column<DateTime>(nullable: false),
                    SaldoConciliado = table.Column<double>(nullable: false),
                    NombreArchivo = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conciliacion", x => x.ConciliacionId);
                    table.ForeignKey(
                        name: "FK_Conciliacion_Bank_IdBanco",
                        column: x => x.IdBanco,
                        principalTable: "Bank",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConciliacionLinea",
                columns: table => new
                {
                    ConciliacionLineaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ElementoConfiguracion = table.Column<long>(nullable: true),
                    Monto = table.Column<double>(nullable: false),
                    ReferenciaBancaria = table.Column<string>(nullable: false),
                    IdMoneda = table.Column<int>(nullable: true),
                    MonedaName = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConciliacionLinea", x => x.ConciliacionLineaId);
                    table.ForeignKey(
                        name: "FK_ConciliacionLinea_ElementoConfiguracion_ElementoConfiguracion",
                        column: x => x.ElementoConfiguracion,
                        principalTable: "ElementoConfiguracion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConciliacionLinea_Currency_IdMoneda",
                        column: x => x.IdMoneda,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryConfigurationLine_JournalEntryConfigurationId",
                table: "JournalEntryConfigurationLine",
                column: "JournalEntryConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Conciliacion_IdBanco",
                table: "Conciliacion",
                column: "IdBanco");

            migrationBuilder.CreateIndex(
                name: "IX_ConciliacionLinea_ElementoConfiguracion",
                table: "ConciliacionLinea",
                column: "ElementoConfiguracion");

            migrationBuilder.CreateIndex(
                name: "IX_ConciliacionLinea_IdMoneda",
                table: "ConciliacionLinea",
                column: "IdMoneda");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntryConfigurationLine_JournalEntryConfiguration_JournalEntryConfigurationId",
                table: "JournalEntryConfigurationLine",
                column: "JournalEntryConfigurationId",
                principalTable: "JournalEntryConfiguration",
                principalColumn: "JournalEntryConfigurationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntryConfigurationLine_JournalEntryConfiguration_JournalEntryConfigurationId",
                table: "JournalEntryConfigurationLine");

            migrationBuilder.DropTable(
                name: "Conciliacion");

            migrationBuilder.DropTable(
                name: "ConciliacionLinea");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntryConfigurationLine_JournalEntryConfigurationId",
                table: "JournalEntryConfigurationLine");
        }
    }
}
