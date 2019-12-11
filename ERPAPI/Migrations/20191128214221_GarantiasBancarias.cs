using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class GarantiasBancarias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarantiaBancaria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    strign = table.Column<string>(nullable: true),
                    FechaInicioVigencia = table.Column<DateTime>(nullable: false),
                    FechaFianlVigencia = table.Column<DateTime>(nullable: false),
                    NumeroCertificado = table.Column<string>(nullable: true),
                    CostCenterId = table.Column<long>(nullable: false),
                    Monto = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Ajuste = table.Column<double>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarantiaBancaria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarantiaBancaria_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GarantiaBancaria_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GarantiaBancaria_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarantiaBancaria_CostCenterId",
                table: "GarantiaBancaria",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_GarantiaBancaria_CurrencyId",
                table: "GarantiaBancaria",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_GarantiaBancaria_IdEstado",
                table: "GarantiaBancaria",
                column: "IdEstado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarantiaBancaria");
        }
    }
}
