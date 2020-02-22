using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FusionKardexDetalleMaestro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TypeOperationId",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "DocumentId",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "CurrencyId",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<double>(
                name: "Currency",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ControlEstibaId",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ControlEstibaName",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CostCenterName",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GoodsReceivedId",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MinimumExistance",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProducId",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QuantityEntry",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QuantityEntryBags",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QuantityEntryCD",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QuantityOut",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QuantityOutBags",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QuantityOutCD",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SaldoAnterior",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SubProducId",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubProductName",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalBags",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalCD",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasureId",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WareHouseId",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WareHouseName",
                table: "Kardex",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "ControlEstibaId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "ControlEstibaName",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "CostCenterName",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "GoodsReceivedId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "MinimumExistance",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "ProducId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "QuantityEntry",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "QuantityEntryBags",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "QuantityEntryCD",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "QuantityOut",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "QuantityOutBags",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "QuantityOutCD",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "SaldoAnterior",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "SubProducId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "SubProductName",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "TotalBags",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "TotalCD",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "WareHouseId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "WareHouseName",
                table: "Kardex");

            migrationBuilder.AlterColumn<int>(
                name: "TypeOperationId",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DocumentId",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CurrencyId",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Currency",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
