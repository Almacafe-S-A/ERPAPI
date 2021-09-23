using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class uniquecustomerterms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CustomerContractTerms_Position_ProductId_TypeInvoiceId",
                table: "CustomerContractTerms",
                columns: new[] { "Position", "ProductId", "TypeInvoiceId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerContractTerms_Position_ProductId_TypeInvoiceId",
                table: "CustomerContractTerms");
        }
    }
}
