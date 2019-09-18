using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Accounting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExchangeRateDecimal",
                table: "ExchangeRate");

            migrationBuilder.AddColumn<long>(
                name: "AccountingAccountId",
                table: "GeneralLedgerLine",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExchangeRateValue",
                table: "ExchangeRate",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<long>(
                name: "AccountingAccountId",
                table: "Account",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Accounting",
                columns: table => new
                {
                    AccountId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentAccountId = table.Column<int>(nullable: true),
                    CompanyInfoId = table.Column<long>(nullable: false),
                    AccountBalance = table.Column<double>(nullable: false),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    IsCash = table.Column<bool>(nullable: false),
                    AccountClasses = table.Column<int>(nullable: false),
                    IsContraAccount = table.Column<bool>(nullable: false),
                    TypeAccountId = table.Column<long>(nullable: false),
                    BlockedInJournal = table.Column<bool>(nullable: false),
                    AccountCode = table.Column<string>(maxLength: 50, nullable: false),
                    HierarchyAccount = table.Column<long>(nullable: false),
                    AccountName = table.Column<string>(maxLength: 200, nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(type: "timestamp", maxLength: 8, nullable: true),
                    ParentAccountAccountId = table.Column<long>(nullable: true),
                    AccountClassid = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounting", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounting_AccountClass_AccountClassid",
                        column: x => x.AccountClassid,
                        principalTable: "AccountClass",
                        principalColumn: "AccountClassid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounting_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "CompanyInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounting_Account_ParentAccountAccountId",
                        column: x => x.ParentAccountAccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerLine_AccountingAccountId",
                table: "GeneralLedgerLine",
                column: "AccountingAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountingAccountId",
                table: "Account",
                column: "AccountingAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounting_AccountClassid",
                table: "Accounting",
                column: "AccountClassid");

            migrationBuilder.CreateIndex(
                name: "IX_Accounting_CompanyInfoId",
                table: "Accounting",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounting_ParentAccountAccountId",
                table: "Accounting",
                column: "ParentAccountAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Accounting_AccountingAccountId",
                table: "Account",
                column: "AccountingAccountId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralLedgerLine_Accounting_AccountingAccountId",
                table: "GeneralLedgerLine",
                column: "AccountingAccountId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Accounting_AccountingAccountId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneralLedgerLine_Accounting_AccountingAccountId",
                table: "GeneralLedgerLine");

            migrationBuilder.DropTable(
                name: "Accounting");

            migrationBuilder.DropIndex(
                name: "IX_GeneralLedgerLine_AccountingAccountId",
                table: "GeneralLedgerLine");

            migrationBuilder.DropIndex(
                name: "IX_Account_AccountingAccountId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "AccountingAccountId",
                table: "GeneralLedgerLine");

            migrationBuilder.DropColumn(
                name: "AccountingAccountId",
                table: "Account");

            migrationBuilder.AlterColumn<double>(
                name: "ExchangeRateValue",
                table: "ExchangeRate",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRateDecimal",
                table: "ExchangeRate",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
