using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class pkterminoscontratos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerContractTerms_Position_ProductId_TypeInvoiceId",
                table: "CustomerContractTerms");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContractTerms_Position_ProductId_TypeInvoiceId_CustomerContractType",
                table: "CustomerContractTerms",
                columns: new[] { "Position", "ProductId", "TypeInvoiceId", "CustomerContractType" },
                unique: true,
                filter: "[CustomerContractType] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerContractTerms_Position_ProductId_TypeInvoiceId_CustomerContractType",
                table: "CustomerContractTerms");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContractTerms_Position_ProductId_TypeInvoiceId",
                table: "CustomerContractTerms",
                columns: new[] { "Position", "ProductId", "TypeInvoiceId" },
                unique: true);
        }
    }
}
