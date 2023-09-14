using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FacturaProveedoresAsientoContable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoConstanciadeRegistro",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "NoFin",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "NoInicio",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "NoOCExenta",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "NoSAG",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "TotalLetras",
                table: "VendorInvoice");

            migrationBuilder.AddColumn<long>(
                name: "JournalEntryId",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoice_JournalEntryId",
                table: "VendorInvoice",
                column: "JournalEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorInvoice_JournalEntry_JournalEntryId",
                table: "VendorInvoice",
                column: "JournalEntryId",
                principalTable: "JournalEntry",
                principalColumn: "JournalEntryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoice_JournalEntry_JournalEntryId",
                table: "VendorInvoice");

            migrationBuilder.DropIndex(
                name: "IX_VendorInvoice_JournalEntryId",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "JournalEntryId",
                table: "VendorInvoice");

            migrationBuilder.AddColumn<string>(
                name: "NoConstanciadeRegistro",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoFin",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoInicio",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoOCExenta",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoSAG",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalLetras",
                table: "VendorInvoice",
                nullable: true);
        }
    }
}
