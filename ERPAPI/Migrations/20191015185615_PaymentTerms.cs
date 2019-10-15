using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PaymentTerms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentTerms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    PaymentTypesId = table.Column<long>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Days = table.Column<int>(nullable: false),
                    Fees = table.Column<int>(nullable: false),
                    FirstPayment = table.Column<double>(nullable: false),
                    EarlyPaymentDiscount = table.Column<double>(nullable: false),
                    AccountingAccountId = table.Column<long>(nullable: true),
                    ChekingAccount = table.Column<string>(nullable: true),
                    Default = table.Column<int>(nullable: false),
                    CustomerPayIn = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTerms_Accounting_AccountingAccountId",
                        column: x => x.AccountingAccountId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentTerms_PaymentTypes_PaymentTypesId",
                        column: x => x.PaymentTypesId,
                        principalTable: "PaymentTypes",
                        principalColumn: "PaymentTypesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTerms_AccountingAccountId",
                table: "PaymentTerms",
                column: "AccountingAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTerms_PaymentTypesId",
                table: "PaymentTerms",
                column: "PaymentTypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentTerms");
        }
    }
}
