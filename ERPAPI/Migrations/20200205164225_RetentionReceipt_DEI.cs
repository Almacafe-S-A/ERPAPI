using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RetentionReceipt_DEI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "RetentionReceipt",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "RetentionReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "RetentionReceipt",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "RetentionReceipt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdPuntoEmision",
                table: "RetentionReceipt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "NumeroDEI",
                table: "RetentionReceipt",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Percentage",
                table: "RetentionReceipt",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RetentionTaxDescription",
                table: "RetentionReceipt",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TaxableBase",
                table: "RetentionReceipt",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalAmount",
                table: "RetentionReceipt",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "IdPuntoEmision",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "NumeroDEI",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "RetentionTaxDescription",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "TaxableBase",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "RetentionReceipt");
        }
    }
}
