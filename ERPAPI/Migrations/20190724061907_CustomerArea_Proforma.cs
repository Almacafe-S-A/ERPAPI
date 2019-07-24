using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerArea_Proforma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerAreaId",
                table: "ProformaInvoice",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "SaldoProducto",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerAreaId",
                table: "ProformaInvoice");

            migrationBuilder.DropColumn(
                name: "SaldoProducto",
                table: "GoodsDeliveryAuthorizationLine");
        }
    }
}
