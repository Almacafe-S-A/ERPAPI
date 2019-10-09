using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DeleteAccountingChilds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountingChilds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountingChilds",
                columns: table => new
                {
                    AccountingChildsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountClasses = table.Column<int>(nullable: false),
                    AccountCode = table.Column<string>(maxLength: 50, nullable: false),
                    AccountName = table.Column<string>(maxLength: 200, nullable: false),
                    AccountingAccountId = table.Column<long>(nullable: true),
                    BlockedInJournal = table.Column<bool>(nullable: false),
                    CompanyInfoId = table.Column<long>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    HierarchyAccount = table.Column<long>(nullable: false),
                    IsCash = table.Column<bool>(nullable: false),
                    IsContraAccount = table.Column<bool>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    ParentAccountId = table.Column<long>(nullable: false),
                    TypeAccountId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingChilds", x => x.AccountingChildsId);
                    table.ForeignKey(
                        name: "FK_AccountingChilds_Accounting_AccountingAccountId",
                        column: x => x.AccountingAccountId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountingChilds_AccountingAccountId",
                table: "AccountingChilds",
                column: "AccountingAccountId");
        }
    }
}
