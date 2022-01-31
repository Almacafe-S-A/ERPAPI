using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class polizaimportacionautorizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DerechoLps",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "TotalCertificado",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "TotalFinanciado",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.AddColumn<string>(
                name: "Aduana",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CartaRetiroDocName",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmpresaSeguro",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LiberacionEndosoDocName",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManifiestoNo",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoTraslado",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URLCartaRetiro",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URLLiberacionEndoso",
                table: "GoodsDeliveryAuthorization",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aduana",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "CartaRetiroDocName",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "EmpresaSeguro",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "LiberacionEndosoDocName",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "ManifiestoNo",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "NoTraslado",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "URLCartaRetiro",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "URLLiberacionEndoso",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.AddColumn<double>(
                name: "DerechoLps",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalCertificado",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalFinanciado",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
