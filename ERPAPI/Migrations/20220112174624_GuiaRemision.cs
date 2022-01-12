using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class GuiaRemision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuiaRemision",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumeroDocumento = table.Column<string>(nullable: true),
                    CAI = table.Column<string>(nullable: true),
                    Rango = table.Column<string>(nullable: true),
                    FechaLimiteEmision = table.Column<DateTime>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomerId1 = table.Column<long>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    Remitente = table.Column<string>(nullable: true),
                    Origen = table.Column<string>(nullable: true),
                    Destino = table.Column<string>(nullable: true),
                    Transportista = table.Column<string>(nullable: true),
                    Vigilante = table.Column<string>(nullable: true),
                    Observaciones = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Placa = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuiaRemision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuiaRemision_Customer_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GuiaRemisionLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GuiaRemisionId = table.Column<int>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: true),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuiaRemisionLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuiaRemisionLines_GuiaRemision_GuiaRemisionId",
                        column: x => x.GuiaRemisionId,
                        principalTable: "GuiaRemision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuiaRemision_CustomerId1",
                table: "GuiaRemision",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_GuiaRemisionLines_GuiaRemisionId",
                table: "GuiaRemisionLines",
                column: "GuiaRemisionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuiaRemisionLines");

            migrationBuilder.DropTable(
                name: "GuiaRemision");
        }
    }
}
