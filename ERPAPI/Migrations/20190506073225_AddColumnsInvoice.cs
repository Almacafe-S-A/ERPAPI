using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddColumnsInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Currency",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "ProformaInvoice",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Currency",
                table: "ProformaInvoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "ProformaInvoice",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "ProformaInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Currency",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Invoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "Invoice",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Invoice",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "ProformaInvoice");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "ProformaInvoice");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProformaInvoice");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "ProformaInvoice");

            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Invoice");
        }
    }
}
