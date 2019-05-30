using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class estadostablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Warehouse",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Warehouse",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "UnitOfMeasure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "TiposDocumento",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "TiposDocumento",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Tax",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Tax",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "IdEstado",
                table: "SalesOrder",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "PuntoEmision",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "PuntoEmision",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "IdEstado",
                table: "ProformaInvoice",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "ProformaInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "ProductType",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "ProductType",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "ProductRelation",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "ProductRelation",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Product",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "PolicyRoles",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "PolicyRoles",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "PolicyClaims",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "PolicyClaims",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Policy",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Policy",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "IdEstado",
                table: "Invoice",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Currency",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "CustomerType",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "CustomerType",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "CustomerConditions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "CustomerConditions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Currency",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Currency",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "ControlPallets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Conditions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Conditions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "CAI",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Branch",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "UnitOfMeasure");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "TiposDocumento");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "TiposDocumento");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "PuntoEmision");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "PuntoEmision");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "ProformaInvoice");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "ProductRelation");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "ProductRelation");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "PolicyRoles");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "PolicyRoles");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "PolicyClaims");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "PolicyClaims");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Policy");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Policy");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "CustomerType");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "CustomerType");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "CustomerConditions");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "CustomerConditions");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Conditions");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Conditions");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "CAI");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Branch");

            migrationBuilder.AlterColumn<int>(
                name: "IdEstado",
                table: "SalesOrder",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "IdEstado",
                table: "ProformaInvoice",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "IdEstado",
                table: "Invoice",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
