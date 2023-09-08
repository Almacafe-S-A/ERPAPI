using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RetencionesVendorInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AplicaRetencion",
                table: "VendorInvoice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RetecionPendiente",
                table: "VendorInvoice",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AplicaRetencion",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "RetecionPendiente",
                table: "VendorInvoice");
        }
    }
}
