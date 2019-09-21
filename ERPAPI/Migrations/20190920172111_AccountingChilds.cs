using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AccountingChilds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountingChilds",
                columns: table => new
                {
                    AccountingChildsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentAccountId = table.Column<int>(nullable: true),
                    CompanyInfoId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    IsCash = table.Column<bool>(nullable: false),
                    AccountClasses = table.Column<int>(nullable: false),
                    IsContraAccount = table.Column<bool>(nullable: false),
                    TypeAccountId = table.Column<long>(nullable: false),
                    BlockedInJournal = table.Column<bool>(nullable: false),
                    AccountCode = table.Column<string>(maxLength: 50, nullable: false),
                    HierarchyAccount = table.Column<long>(nullable: false),
                    AccountName = table.Column<string>(maxLength: 200, nullable: false),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    AccountingAccountId = table.Column<long>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountingChilds");
        }
    }
}
