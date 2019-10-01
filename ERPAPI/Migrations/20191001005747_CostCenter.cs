using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CostCenter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Num",
                table: "JournalEntryLine");

            migrationBuilder.RenameColumn(
                name: "CenterCostName",
                table: "ProformaInvoiceLine",
                newName: "CostCenterName");

            migrationBuilder.RenameColumn(
                name: "CenterCostId",
                table: "ProformaInvoiceLine",
                newName: "CostCenterId");

            migrationBuilder.RenameColumn(
                name: "CenterCostName",
                table: "JournalEntryConfigurationLine",
                newName: "CostCenterName");

            migrationBuilder.RenameColumn(
                name: "CenterCostId",
                table: "JournalEntryConfigurationLine",
                newName: "CostCenterId");

            migrationBuilder.RenameColumn(
                name: "CenterCostName",
                table: "InvoiceLine",
                newName: "CostCenterName");

            migrationBuilder.RenameColumn(
                name: "CenterCostId",
                table: "InvoiceLine",
                newName: "CostCenterId");

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "JournalEntryLine",
                maxLength: 30,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CostCenterName",
                table: "JournalEntryLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "CustomerArea",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "CustomerArea",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CostCenter",
                columns: table => new
                {
                    CostCenterId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CostCenterName = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenter", x => x.CostCenterId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostCenter");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "JournalEntryLine");

            migrationBuilder.DropColumn(
                name: "CostCenterName",
                table: "JournalEntryLine");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CustomerArea");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "CustomerArea");

            migrationBuilder.RenameColumn(
                name: "CostCenterName",
                table: "ProformaInvoiceLine",
                newName: "CenterCostName");

            migrationBuilder.RenameColumn(
                name: "CostCenterId",
                table: "ProformaInvoiceLine",
                newName: "CenterCostId");

            migrationBuilder.RenameColumn(
                name: "CostCenterName",
                table: "JournalEntryConfigurationLine",
                newName: "CenterCostName");

            migrationBuilder.RenameColumn(
                name: "CostCenterId",
                table: "JournalEntryConfigurationLine",
                newName: "CenterCostId");

            migrationBuilder.RenameColumn(
                name: "CostCenterName",
                table: "InvoiceLine",
                newName: "CenterCostName");

            migrationBuilder.RenameColumn(
                name: "CostCenterId",
                table: "InvoiceLine",
                newName: "CenterCostId");

            migrationBuilder.AddColumn<string>(
                name: "Num",
                table: "JournalEntryLine",
                maxLength: 30,
                nullable: true);
        }
    }
}
