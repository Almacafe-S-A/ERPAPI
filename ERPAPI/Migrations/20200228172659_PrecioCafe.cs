using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PrecioCafe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrecioCafe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    PrecioBolsaUSD = table.Column<decimal>(nullable: false),
                    DiferencialesUSD = table.Column<decimal>(nullable: false),
                    TotalUSD = table.Column<decimal>(nullable: false),
                    ExchangeRateId = table.Column<long>(nullable: false),
                    BrutoLPSIngreso = table.Column<decimal>(nullable: false),
                    PorcentajeIngreso = table.Column<decimal>(nullable: false),
                    NetoLPSIngreso = table.Column<decimal>(nullable: false),
                    BrutoLPSConsumoInterno = table.Column<decimal>(nullable: false),
                    PorcentajeConsumoInterno = table.Column<decimal>(nullable: false),
                    NetoLPSConsumoInterno = table.Column<decimal>(nullable: false),
                    TotalLPSIngreso = table.Column<decimal>(nullable: false),
                    BeneficiadoUSD = table.Column<decimal>(nullable: false),
                    FideicomisoUSD = table.Column<decimal>(nullable: false),
                    UtilidadUSD = table.Column<decimal>(nullable: false),
                    PermisoExportacionUSD = table.Column<decimal>(nullable: false),
                    TotalUSDEgreso = table.Column<decimal>(nullable: false),
                    TotalLPSEgreso = table.Column<decimal>(nullable: false),
                    PrecioQQOro = table.Column<decimal>(nullable: false),
                    PercioQQPergamino = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrecioCafe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrecioCafe_ExchangeRate_ExchangeRateId",
                        column: x => x.ExchangeRateId,
                        principalTable: "ExchangeRate",
                        principalColumn: "ExchangeRateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrecioCafe_ExchangeRateId",
                table: "PrecioCafe",
                column: "ExchangeRateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrecioCafe");
        }
    }
}
