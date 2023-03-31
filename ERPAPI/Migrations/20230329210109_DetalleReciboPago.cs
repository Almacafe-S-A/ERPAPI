using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DetalleReciboPago : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Saldo",
                table: "InvoiceLine",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "InvoicePaymentsLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoicePaymentId = table.Column<int>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    ValorOriginal = table.Column<decimal>(nullable: false),
                    MontoAdeudaPrevio = table.Column<decimal>(nullable: false),
                    MontoPagado = table.Column<decimal>(nullable: false),
                    MontoRestante = table.Column<decimal>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentsLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicePaymentsLine_InvoicePayments_InvoicePaymentId",
                        column: x => x.InvoicePaymentId,
                        principalTable: "InvoicePayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoicePaymentsLine_SubProduct_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProduct",
                        principalColumn: "SubproductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentsLine_InvoicePaymentId",
                table: "InvoicePaymentsLine",
                column: "InvoicePaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentsLine_SubProductId",
                table: "InvoicePaymentsLine",
                column: "SubProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicePaymentsLine");

            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "InvoiceLine");
        }
    }
}
