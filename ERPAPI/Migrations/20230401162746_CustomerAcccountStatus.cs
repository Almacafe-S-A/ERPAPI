using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerAcccountStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerAcccountStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreditNoteTypeId = table.Column<int>(nullable: false),
                    InvoiceId = table.Column<int>(nullable: true),
                    InvoicePaymentId = table.Column<int>(nullable: false),
                    Debito = table.Column<decimal>(nullable: false),
                    Credito = table.Column<decimal>(nullable: false),
                    Saldo = table.Column<decimal>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAcccountStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAcccountStatus_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAcccountStatus_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerAcccountStatus_InvoicePayments_InvoicePaymentId",
                        column: x => x.InvoicePaymentId,
                        principalTable: "InvoicePayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAcccountStatus_CustomerId",
                table: "CustomerAcccountStatus",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAcccountStatus_InvoiceId",
                table: "CustomerAcccountStatus",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAcccountStatus_InvoicePaymentId",
                table: "CustomerAcccountStatus",
                column: "InvoicePaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAcccountStatus");
        }
    }
}
