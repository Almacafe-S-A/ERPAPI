using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Kardex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "DocName",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "GoodsReceivedId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "MinimumExistance",
                table: "Kardex");

            migrationBuilder.AlterColumn<long>(
                name: "WareHouseId",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "TotalCD",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "TotalBags",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<long>(
                name: "SubProducId",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "BranchId",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WareHouseId",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TotalCD",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TotalBags",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubProducId",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BranchId",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Currency",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocName",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GoodsReceivedId",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MinimumExistance",
                table: "Kardex",
                nullable: true);
        }
    }
}
