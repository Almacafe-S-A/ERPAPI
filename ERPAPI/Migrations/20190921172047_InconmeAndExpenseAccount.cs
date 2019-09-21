using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InconmeAndExpenseAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncomeAndExpenseAccountLine",
                columns: table => new
                {
                    IncomeAndExpenseAccountLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IncomeAndExpensesAccountId = table.Column<long>(nullable: false),
                    TypeDocument = table.Column<long>(nullable: false),
                    DocumentId = table.Column<long>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    DebitCredit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeAndExpenseAccountLine", x => x.IncomeAndExpenseAccountLineId);
                });

            migrationBuilder.CreateTable(
                name: "IncomeAndExpensesAccount",
                columns: table => new
                {
                    IncomeAndExpensesAccountId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    TypeAccountId = table.Column<long>(nullable: false),
                    TypeAccountName = table.Column<string>(nullable: true),
                    AccountDescription = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: false),
                    EstadoName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeAndExpensesAccount", x => x.IncomeAndExpensesAccountId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncomeAndExpenseAccountLine");

            migrationBuilder.DropTable(
                name: "IncomeAndExpensesAccount");
        }
    }
}
