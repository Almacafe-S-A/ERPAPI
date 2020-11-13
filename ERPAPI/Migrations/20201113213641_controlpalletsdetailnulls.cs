using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class controlpalletsdetailnulls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlPallets_Product_ProductId",
                table: "ControlPallets");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlPallets_SubProduct_SubProductId",
                table: "ControlPallets");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "ControlPallets",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "SubProductId",
                table: "ControlPallets",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "ControlPallets",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPallets_Product_ProductId",
                table: "ControlPallets",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPallets_SubProduct_SubProductId",
                table: "ControlPallets",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlPallets_Product_ProductId",
                table: "ControlPallets");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlPallets_SubProduct_SubProductId",
                table: "ControlPallets");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "ControlPallets",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SubProductId",
                table: "ControlPallets",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "ControlPallets",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPallets_Product_ProductId",
                table: "ControlPallets",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPallets_SubProduct_SubProductId",
                table: "ControlPallets",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
