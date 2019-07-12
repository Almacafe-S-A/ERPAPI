using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ImpresionDocumentos_Sacos_Quintales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "ProformaInvoice",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QuantityEntryBags",
                table: "KardexLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "QuantityEntryCD",
                table: "KardexLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "QuantityOutBags",
                table: "KardexLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "QuantityOutCD",
                table: "KardexLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalBags",
                table: "KardexLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalCD",
                table: "KardexLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "GoodsDelivered",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "EndososTalon",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "EndososCertificados",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "EndososBono",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "CustomerArea",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasureId",
                table: "CustomerArea",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "CustomerArea",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cantidadPoliEtileno",
                table: "ControlPalletsLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cantidadYute",
                table: "ControlPalletsLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalSacosYute",
                table: "ControlPallets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "CertificadoDeposito",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "ProformaInvoice");

            migrationBuilder.DropColumn(
                name: "QuantityEntryBags",
                table: "KardexLine");

            migrationBuilder.DropColumn(
                name: "QuantityEntryCD",
                table: "KardexLine");

            migrationBuilder.DropColumn(
                name: "QuantityOutBags",
                table: "KardexLine");

            migrationBuilder.DropColumn(
                name: "QuantityOutCD",
                table: "KardexLine");

            migrationBuilder.DropColumn(
                name: "TotalBags",
                table: "KardexLine");

            migrationBuilder.DropColumn(
                name: "TotalCD",
                table: "KardexLine");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "GoodsDelivered");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "EndososTalon");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "EndososCertificados");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "EndososBono");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "CustomerArea");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "CustomerArea");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "CustomerArea");

            migrationBuilder.DropColumn(
                name: "cantidadPoliEtileno",
                table: "ControlPalletsLine");

            migrationBuilder.DropColumn(
                name: "cantidadYute",
                table: "ControlPalletsLine");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "TotalSacosYute",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "CertificadoDeposito");
        }
    }
}
