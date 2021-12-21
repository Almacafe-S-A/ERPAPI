using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CambiosAutorizacionSalida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CertificadoLineId",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "Saldo",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificadoLineId",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "GoodsDeliveryAuthorizationLine");
        }
    }
}
