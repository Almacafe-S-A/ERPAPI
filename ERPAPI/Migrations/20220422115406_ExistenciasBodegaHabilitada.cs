using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ExistenciasBodegaHabilitada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisponibleBodegaHabilitadas");

            migrationBuilder.CreateTable(
                name: "ExistenciaBodegaHabilitadas",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    InventarioBodegaHabilitadaId = table.Column<int>(nullable: true),
                    InventarioBodegaHabilidaId = table.Column<int>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    ExistenciaTotal = table.Column<decimal>(nullable: false),
                    IngresandoHoy = table.Column<decimal>(nullable: false),
                    DisponibleCertificar = table.Column<decimal>(nullable: false),
                    RetencionesCafeInferiores = table.Column<decimal>(nullable: false),
                    Subtotal = table.Column<decimal>(nullable: false),
                    RetencionesMerma = table.Column<decimal>(nullable: false),
                    TotalCertificado = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExistenciaBodegaHabilitadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExistenciaBodegaHabilitadas_InventarioBodegaHabilitada_InventarioBodegaHabilitadaId",
                        column: x => x.InventarioBodegaHabilitadaId,
                        principalTable: "InventarioBodegaHabilitada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExistenciaBodegaHabilitadas_SubProduct_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProduct",
                        principalColumn: "SubproductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExistenciaBodegaHabilitadas_InventarioBodegaHabilitadaId",
                table: "ExistenciaBodegaHabilitadas",
                column: "InventarioBodegaHabilitadaId");

            migrationBuilder.CreateIndex(
                name: "IX_ExistenciaBodegaHabilitadas_SubProductId",
                table: "ExistenciaBodegaHabilitadas",
                column: "SubProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExistenciaBodegaHabilitadas");

            migrationBuilder.CreateTable(
                name: "DisponibleBodegaHabilitadas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Certificado = table.Column<decimal>(nullable: false),
                    DisponibleCertificar = table.Column<decimal>(nullable: false),
                    Existencias = table.Column<decimal>(nullable: false),
                    IngersoHoy = table.Column<decimal>(nullable: false),
                    InventarioBodegaHabilitadaId = table.Column<int>(nullable: false),
                    InventarioFisico = table.Column<decimal>(nullable: false),
                    Merma = table.Column<decimal>(nullable: false),
                    PrductName = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    Retencion7 = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisponibleBodegaHabilitadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisponibleBodegaHabilitadas_InventarioBodegaHabilitada_InventarioBodegaHabilitadaId",
                        column: x => x.InventarioBodegaHabilitadaId,
                        principalTable: "InventarioBodegaHabilitada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisponibleBodegaHabilitadas_InventarioBodegaHabilitadaId",
                table: "DisponibleBodegaHabilitadas",
                column: "InventarioBodegaHabilitadaId");
        }
    }
}
