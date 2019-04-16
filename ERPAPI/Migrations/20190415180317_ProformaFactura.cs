using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ProformaFactura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "SalesOrderLine",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "SalesOrder",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "SalesOrder",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Tax18",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalGravado18",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Invoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CAI",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Caja",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Invoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Invoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CustomerRefNumber",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Invoice",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Invoice",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaLimiteEmision",
                table: "Invoice",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Invoice",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Freight",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "IdEstado",
                table: "Invoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NoConstanciadeRegistro",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoFin",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoInicio",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoOCExenta",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoSAG",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroDEI",
                table: "Invoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Invoice",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RTN",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalesTypeId",
                table: "Invoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SubTotal",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Sucursal",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Tax",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Tax18",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tefono",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoDocumento",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalExento",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalExonerado",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalGravado",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalGravado18",
                table: "Invoice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "Invoice",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InvoiceLine",
                columns: table => new
                {
                    SalesOrderLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SalesOrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    DiscountPercentage = table.Column<double>(nullable: false),
                    DiscountAmount = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    TaxPercentage = table.Column<double>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxAmount = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLine", x => x.SalesOrderLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLine_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrder",
                        principalColumn: "SalesOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProformaInvoice",
                columns: table => new
                {
                    SalesOrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SalesOrderName = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    Tefono = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    BranchId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    CustomerRefNumber = table.Column<string>(nullable: true),
                    SalesTypeId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    Tax18 = table.Column<double>(nullable: false),
                    Freight = table.Column<double>(nullable: false),
                    TotalExento = table.Column<double>(nullable: false),
                    TotalExonerado = table.Column<double>(nullable: false),
                    TotalGravado = table.Column<double>(nullable: false),
                    TotalGravado18 = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    IdEstado = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProformaInvoice", x => x.SalesOrderId);
                });

            migrationBuilder.CreateTable(
                name: "ProformaInvoiceLine",
                columns: table => new
                {
                    ProformaLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProformaInvoiceId = table.Column<int>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    DiscountPercentage = table.Column<double>(nullable: false),
                    DiscountAmount = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    TaxPercentage = table.Column<double>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxAmount = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProformaInvoiceLine", x => x.ProformaLineId);
                    table.ForeignKey(
                        name: "FK_ProformaInvoiceLine_ProformaInvoice_ProformaInvoiceId",
                        column: x => x.ProformaInvoiceId,
                        principalTable: "ProformaInvoice",
                        principalColumn: "SalesOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_InvoiceId",
                table: "SalesOrderLine",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_SalesOrderId",
                table: "InvoiceLine",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProformaInvoiceLine_ProformaInvoiceId",
                table: "ProformaInvoiceLine",
                column: "ProformaInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_Invoice_InvoiceId",
                table: "SalesOrderLine",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_Invoice_InvoiceId",
                table: "SalesOrderLine");

            migrationBuilder.DropTable(
                name: "InvoiceLine");

            migrationBuilder.DropTable(
                name: "ProformaInvoiceLine");

            migrationBuilder.DropTable(
                name: "ProformaInvoice");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLine_InvoiceId",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Tax18",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "TotalGravado18",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CAI",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Caja",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerRefNumber",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "FechaLimiteEmision",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Freight",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "NoConstanciadeRegistro",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "NoFin",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "NoInicio",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "NoOCExenta",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "NoSAG",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "NumeroDEI",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "RTN",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "SalesTypeId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Sucursal",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Tax18",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Tefono",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "TipoDocumento",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "TotalExento",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "TotalExonerado",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "TotalGravado",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "TotalGravado18",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "Invoice");
        }
    }
}
