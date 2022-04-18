using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DisponibilidadBH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisponibleBodegaHabilitadas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InventarioBodegaHabilitadaId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    PrductName = table.Column<string>(nullable: true),
                    Existencias = table.Column<decimal>(nullable: false),
                    IngersoHoy = table.Column<decimal>(nullable: false),
                    InventarioFisico = table.Column<decimal>(nullable: false),
                    Retencion7 = table.Column<decimal>(nullable: false),
                    Merma = table.Column<decimal>(nullable: false),
                    DisponibleCertificar = table.Column<decimal>(nullable: false),
                    Certificado = table.Column<decimal>(nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisponibleBodegaHabilitadas");
        }
    }
}
