using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Pagosfactura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoicePayments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NoPago = table.Column<int>(nullable: false),
                    FechaPago = table.Column<DateTime>(nullable: false),
                    InvoivceId = table.Column<int>(nullable: false),
                    MontoAdeudaPrevio = table.Column<decimal>(nullable: false),
                    MontoPagado = table.Column<decimal>(nullable: false),
                    MontoAdeudado = table.Column<decimal>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    Depositante = table.Column<string>(nullable: true),
                    Referrencia = table.Column<string>(nullable: true),
                    CuentaBancariaId = table.Column<long>(nullable: true),
                    Bank = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    TipoPago = table.Column<int>(nullable: false),
                    JournalId = table.Column<long>(nullable: false),
                    EstadoId = table.Column<int>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicePayments_AccountManagement_CuentaBancariaId",
                        column: x => x.CuentaBancariaId,
                        principalTable: "AccountManagement",
                        principalColumn: "AccountManagementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoicePayments_Invoice_InvoivceId",
                        column: x => x.InvoivceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoicePayments_JournalEntry_JournalId",
                        column: x => x.JournalId,
                        principalTable: "JournalEntry",
                        principalColumn: "JournalEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayments_CuentaBancariaId",
                table: "InvoicePayments",
                column: "CuentaBancariaId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayments_InvoivceId",
                table: "InvoicePayments",
                column: "InvoivceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayments_JournalId",
                table: "InvoicePayments",
                column: "JournalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicePayments");
        }
    }
}
