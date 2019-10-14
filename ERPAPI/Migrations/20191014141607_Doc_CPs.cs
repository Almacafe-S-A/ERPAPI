using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Doc_CPs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductTypeId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    PaymentTypesId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaymentTypesName = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.PaymentTypesId);
                });

            migrationBuilder.CreateTable(
                name: "Doc_CP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DocNumber = table.Column<string>(nullable: true),
                    DocTypeId = table.Column<int>(nullable: false),
                    DocTipoId = table.Column<long>(nullable: true),
                    DocDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    PartialAmount = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    PaymentQty = table.Column<int>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    Balance_Mon = table.Column<double>(nullable: false),
                    DocPaymentNumber = table.Column<string>(nullable: true),
                    Payed = table.Column<bool>(nullable: false),
                    LatePaymentAmount = table.Column<double>(nullable: false),
                    LatePaymentInterest = table.Column<double>(nullable: false),
                    DayTerms = table.Column<int>(nullable: false),
                    VendorDocumentId = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    AnnulationReason = table.Column<string>(nullable: true),
                    TaxId = table.Column<long>(nullable: false),
                    PaymentTypeId = table.Column<long>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    Base = table.Column<bool>(nullable: false),
                    PaymentNumber = table.Column<string>(nullable: true),
                    PaymentReference = table.Column<string>(nullable: true),
                    PaymentTerm = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doc_CP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doc_CP_Accounting_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doc_CP_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doc_CP_ElementoConfiguracion_DocTipoId",
                        column: x => x.DocTipoId,
                        principalTable: "ElementoConfiguracion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doc_CP_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "PaymentTypesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doc_CP_Tax_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Doc_CP_AccountId",
                table: "Doc_CP",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Doc_CP_CurrencyId",
                table: "Doc_CP",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Doc_CP_DocTipoId",
                table: "Doc_CP",
                column: "DocTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Doc_CP_PaymentTypeId",
                table: "Doc_CP",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Doc_CP_TaxId",
                table: "Doc_CP",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ElementoConfiguracion_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                principalTable: "ElementoConfiguracion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ElementoConfiguracion_ProductTypeId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Doc_CP");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Product");
        }
    }
}
