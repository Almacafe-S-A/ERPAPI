using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DeleteAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounting_Account_ParentAccountAccountId",
                table: "Accounting");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneralLedgerLine_Account_AccountId1",
                table: "GeneralLedgerLine");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneralLedgerLine_Accounting_AccountingAccountId",
                table: "GeneralLedgerLine");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntryLine_Account_AccountId1",
                table: "JournalEntryLine");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropIndex(
                name: "IX_GeneralLedgerLine_AccountingAccountId",
                table: "GeneralLedgerLine");

            migrationBuilder.DropColumn(
                name: "AccountingAccountId",
                table: "GeneralLedgerLine");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounting_Accounting_ParentAccountAccountId",
                table: "Accounting",
                column: "ParentAccountAccountId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralLedgerLine_Accounting_AccountId1",
                table: "GeneralLedgerLine",
                column: "AccountId1",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntryLine_Accounting_AccountId1",
                table: "JournalEntryLine",
                column: "AccountId1",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounting_Accounting_ParentAccountAccountId",
                table: "Accounting");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneralLedgerLine_Accounting_AccountId1",
                table: "GeneralLedgerLine");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntryLine_Accounting_AccountId1",
                table: "JournalEntryLine");

            migrationBuilder.AddColumn<long>(
                name: "AccountingAccountId",
                table: "GeneralLedgerLine",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountBalance = table.Column<double>(nullable: false),
                    AccountClasses = table.Column<int>(nullable: false),
                    AccountClassid = table.Column<long>(nullable: true),
                    AccountCode = table.Column<string>(maxLength: 50, nullable: false),
                    AccountName = table.Column<string>(maxLength: 200, nullable: false),
                    AccountingAccountId = table.Column<long>(nullable: true),
                    BlockedInJournal = table.Column<bool>(nullable: false),
                    CompanyInfoId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    HierarchyAccount = table.Column<long>(nullable: false),
                    IsCash = table.Column<bool>(nullable: false),
                    IsContraAccount = table.Column<bool>(nullable: false),
                    ParentAccountAccountId = table.Column<long>(nullable: true),
                    ParentAccountId = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(type: "timestamp", maxLength: 8, nullable: true),
                    TypeAccountId = table.Column<long>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Account_AccountClass_AccountClassid",
                        column: x => x.AccountClassid,
                        principalTable: "AccountClass",
                        principalColumn: "AccountClassid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Account_Accounting_AccountingAccountId",
                        column: x => x.AccountingAccountId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Account_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "CompanyInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Account_Account_ParentAccountAccountId",
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
                name: "IX_Account_AccountClassid",
                table: "Account",
                column: "AccountClassid");

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountingAccountId",
                table: "Account",
                column: "AccountingAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_CompanyInfoId",
                table: "Account",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_ParentAccountAccountId",
                table: "Account",
                column: "ParentAccountAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounting_Account_ParentAccountAccountId",
                table: "Accounting",
                column: "ParentAccountAccountId",
                principalTable: "Account",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralLedgerLine_Account_AccountId1",
                table: "GeneralLedgerLine",
                column: "AccountId1",
                principalTable: "Account",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralLedgerLine_Accounting_AccountingAccountId",
                table: "GeneralLedgerLine",
                column: "AccountingAccountId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntryLine_Account_AccountId1",
                table: "JournalEntryLine",
                column: "AccountId1",
                principalTable: "Account",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
