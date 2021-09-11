using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerContractTermsx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerContractTerms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Position = table.Column<int>(nullable: false),
                    TermTitle = table.Column<string>(nullable: true),
                    Term = table.Column<string>(nullable: true),
                    TypeInvoiceId = table.Column<long>(nullable: false),
                    TypeInvoiceName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContractTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContractTerms_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContractLinesTerms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Position = table.Column<int>(nullable: false),
                    ContractTermId = table.Column<int>(nullable: false),
                    TermTitle = table.Column<string>(nullable: true),
                    Term = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContractLinesTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContractLinesTerms_CustomerContractTerms_ContractTermId",
                        column: x => x.ContractTermId,
                        principalTable: "CustomerContractTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContractLinesTerms_ContractTermId",
                table: "CustomerContractLinesTerms",
                column: "ContractTermId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContractTerms_ProductId",
                table: "CustomerContractTerms",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerContractLinesTerms");

            migrationBuilder.DropTable(
                name: "CustomerContractTerms");
        }
    }
}
