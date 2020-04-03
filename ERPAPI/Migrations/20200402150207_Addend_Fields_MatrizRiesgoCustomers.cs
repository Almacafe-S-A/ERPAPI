using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Addend_Fields_MatrizRiesgoCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatrizRiesgoCustomers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    IdFactorRiesgo = table.Column<long>(nullable: false),
                    FactorRiesgo = table.Column<string>(nullable: true),
                    Riesgo = table.Column<string>(nullable: true),
                    Efecto = table.Column<string>(nullable: true),
                    IdContextoRiesgo = table.Column<long>(nullable: false),
                    Responsable = table.Column<string>(nullable: true),
                    RiesgoInicialProbabilidad = table.Column<long>(nullable: false),
                    RiesgoInicialImpacto = table.Column<long>(nullable: false),
                    RiesgoInicialCalificacion = table.Column<long>(nullable: false),
                    RiesgoInicialValorSeveridad = table.Column<long>(nullable: false),
                    RiesgoInicialNivel = table.Column<string>(nullable: true),
                    RiesgoInicialColorHexadecimal = table.Column<string>(nullable: true),
                    Controles = table.Column<string>(nullable: true),
                    TipodeAccionderiesgoId = table.Column<int>(nullable: false),
                    FechaObjetvo = table.Column<string>(nullable: true),
                    RiesgoResidualProbabilidad = table.Column<long>(nullable: false),
                    RiesgoResidualImpacto = table.Column<long>(nullable: false),
                    RiesgoResidualCalificacion = table.Column<long>(nullable: false),
                    RiesgoResidualValorSeveridad = table.Column<long>(nullable: false),
                    RiesgoResidualNivel = table.Column<string>(nullable: true),
                    RiesgoResidualColorHexadecimal = table.Column<string>(nullable: true),
                    Seguimiento = table.Column<string>(nullable: true),
                    FechaRevision = table.Column<DateTime>(nullable: false),
                    Avance = table.Column<double>(nullable: false),
                    Eficaz = table.Column<bool>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatrizRiesgoCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatrizRiesgoCustomers_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatrizRiesgoCustomers_ContextoRiesgo_IdContextoRiesgo",
                        column: x => x.IdContextoRiesgo,
                        principalTable: "ContextoRiesgo",
                        principalColumn: "IdContextoRiesgo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatrizRiesgoCustomers_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatrizRiesgoCustomers_TipodeAccionderiesgo_TipodeAccionderiesgoId",
                        column: x => x.TipodeAccionderiesgoId,
                        principalTable: "TipodeAccionderiesgo",
                        principalColumn: "TipodeAccionderiesgoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatrizRiesgoCustomers_CustomerId",
                table: "MatrizRiesgoCustomers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatrizRiesgoCustomers_IdContextoRiesgo",
                table: "MatrizRiesgoCustomers",
                column: "IdContextoRiesgo");

            migrationBuilder.CreateIndex(
                name: "IX_MatrizRiesgoCustomers_ProductId",
                table: "MatrizRiesgoCustomers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MatrizRiesgoCustomers_TipodeAccionderiesgoId",
                table: "MatrizRiesgoCustomers",
                column: "TipodeAccionderiesgoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatrizRiesgoCustomers");
        }
    }
}
