using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class centercost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CenterCostName",
                table: "ProformaInvoiceLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "ProformaInvoice",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CenterCostId",
                table: "KardexLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CenterCostName",
                table: "KardexLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasureId",
                table: "KardexLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "KardexLine",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Currency",
                table: "Kardex",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Kardex",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CenterCostName",
                table: "InvoiceLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WareHouseName",
                table: "GoodsReceivedLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CenterCostId",
                table: "ControlPalletsLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CenterCostName",
                table: "ControlPalletsLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CenterCostId",
                table: "CertificadoLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CenterCostName",
                table: "CertificadoLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "CenterCostName",
                table: "ProformaInvoiceLine");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "ProformaInvoice");

            migrationBuilder.DropColumn(
                name: "CenterCostId",
                table: "KardexLine");

            migrationBuilder.DropColumn(
                name: "CenterCostName",
                table: "KardexLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "KardexLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "KardexLine");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "CenterCostName",
                table: "InvoiceLine");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "WareHouseName",
                table: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "CenterCostId",
                table: "ControlPalletsLine");

            migrationBuilder.DropColumn(
                name: "CenterCostName",
                table: "ControlPalletsLine");

            migrationBuilder.DropColumn(
                name: "CenterCostId",
                table: "CertificadoLine");

            migrationBuilder.DropColumn(
                name: "CenterCostName",
                table: "CertificadoLine");
        }
    }
}
