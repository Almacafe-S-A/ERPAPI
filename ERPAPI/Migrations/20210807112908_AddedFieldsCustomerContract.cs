using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFieldsCustomerContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ComisionMax",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ComisionMin",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameContract",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PolizaPropia",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioBaseProducto",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioServicio",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TypeContractId",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TypeInvoiceId",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "TypeInvoiceName",
                table: "CustomerContract",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComisionMax",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "ComisionMin",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "NameContract",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "PolizaPropia",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "PrecioBaseProducto",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "PrecioServicio",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "TypeContractId",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "TypeInvoiceId",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "TypeInvoiceName",
                table: "CustomerContract");
        }
    }
}
