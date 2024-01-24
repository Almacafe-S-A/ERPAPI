using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ARFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aduana",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "EmpresaSeguro",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.RenameColumn(
                name: "NoTraslado",
                table: "GoodsDeliveryAuthorization",
                newName: "UsuarioRevisor");

            migrationBuilder.RenameColumn(
                name: "NoPoliza",
                table: "GoodsDeliveryAuthorization",
                newName: "NoPolizaImportacion");

            migrationBuilder.RenameColumn(
                name: "ManifiestoNo",
                table: "GoodsDeliveryAuthorization",
                newName: "UsuarioAprobacion");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAutorizado",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCantidad",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDerechos",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAutorizado",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "TotalCantidad",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "TotalDerechos",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.RenameColumn(
                name: "UsuarioRevisor",
                table: "GoodsDeliveryAuthorization",
                newName: "NoTraslado");

            migrationBuilder.RenameColumn(
                name: "UsuarioAprobacion",
                table: "GoodsDeliveryAuthorization",
                newName: "ManifiestoNo");

            migrationBuilder.RenameColumn(
                name: "NoPolizaImportacion",
                table: "GoodsDeliveryAuthorization",
                newName: "NoPoliza");

            migrationBuilder.AddColumn<string>(
                name: "Aduana",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmpresaSeguro",
                table: "GoodsDeliveryAuthorization",
                nullable: true);
        }
    }
}
