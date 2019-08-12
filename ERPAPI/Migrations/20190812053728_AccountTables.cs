using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AccountTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountClass",
                columns: table => new
                {
                    AccountClassid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NormalBalance = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClass", x => x.AccountClassid);
                });

            migrationBuilder.CreateTable(
                name: "GeneralLedgerHeader",
                columns: table => new
                {
                    GeneralLedgerHeaderId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    DocumentType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralLedgerHeader", x => x.GeneralLedgerHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentAccountId = table.Column<int>(nullable: true),
                    CompanyInfoId = table.Column<long>(nullable: false),
                    AccountCode = table.Column<string>(maxLength: 50, nullable: false),
                    AccountName = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    IsCash = table.Column<bool>(nullable: false),
                    IsContraAccount = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(type: "timestamp", maxLength: 8, nullable: true),
                    ParentAccountAccountId = table.Column<long>(nullable: true),
                    AccountClassid = table.Column<long>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "GeneralLedgerLine",
                columns: table => new
                {
                    GeneralLedgerHeaderId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    DrCr = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    AccountId1 = table.Column<long>(nullable: true),
                    GeneralLedgerHeaderId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralLedgerLine", x => x.GeneralLedgerHeaderId);
                    table.ForeignKey(
                        name: "FK_GeneralLedgerLine_Account_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneralLedgerLine_GeneralLedgerHeader_GeneralLedgerHeaderId1",
                        column: x => x.GeneralLedgerHeaderId1,
                        principalTable: "GeneralLedgerHeader",
                        principalColumn: "GeneralLedgerHeaderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountClassid",
                table: "Account",
                column: "AccountClassid");

            migrationBuilder.CreateIndex(
                name: "IX_Account_CompanyInfoId",
                table: "Account",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_ParentAccountAccountId",
                table: "Account",
                column: "ParentAccountAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerLine_AccountId1",
                table: "GeneralLedgerLine",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerLine_GeneralLedgerHeaderId1",
                table: "GeneralLedgerLine",
                column: "GeneralLedgerHeaderId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralLedgerLine");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "GeneralLedgerHeader");

            migrationBuilder.DropTable(
                name: "AccountClass");
        }
    }
}
