using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CostListItemExchangeRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostListItem",
                columns: table => new
                {
                    CostListItemId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SubproductId = table.Column<long>(nullable: false),
                    ExchangeRateId = table.Column<long>(nullable: false),
                    DayofCalcule = table.Column<DateTime>(nullable: false),
                    PriceBagValue = table.Column<double>(nullable: false),
                    DifferencyPriceBagValue = table.Column<double>(nullable: false),
                    TotalPriceBagValue = table.Column<double>(nullable: false),
                    PriceBagValueCurrency = table.Column<double>(nullable: false),
                    PorcentagePriceBagValue = table.Column<double>(nullable: false),
                    RealBagValueCurrency = table.Column<double>(nullable: false),
                    ConRealBagValueInside = table.Column<double>(nullable: false),
                    PCRealBagValueInside = table.Column<double>(nullable: false),
                    RealBagValueInside = table.Column<double>(nullable: false),
                    TotalIncomes = table.Column<double>(nullable: false),
                    RecipientExpenses = table.Column<double>(nullable: false),
                    EscrowExpenses = table.Column<double>(nullable: false),
                    UtilityExpenses = table.Column<double>(nullable: false),
                    PermiseExportExpenses = table.Column<double>(nullable: false),
                    TaxesExpenses = table.Column<double>(nullable: false),
                    TotalExpenses = table.Column<double>(nullable: false),
                    TotalExpensesCurrency = table.Column<double>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostListItem", x => x.CostListItemId);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRate",
                columns: table => new
                {
                    ExchangeRateId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayofRate = table.Column<DateTime>(nullable: false),
                    ExchangeRateValue = table.Column<double>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRate", x => x.ExchangeRateId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostListItem");

            migrationBuilder.DropTable(
                name: "ExchangeRate");
        }
    }
}
