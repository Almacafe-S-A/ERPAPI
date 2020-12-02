using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InsuracesCerticates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModifiedUser",
                table: "ExchangeRate",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUser",
                table: "ExchangeRate",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<long>(
                name: "ProductTypeId",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EstadoId",
                table: "BankAccountTransfers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EstadosIdEstado",
                table: "BankAccountTransfers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InsuranceCertificate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    InsuranceId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    AmountWords = table.Column<string>(nullable: true),
                    ProductTypeId = table.Column<long>(nullable: true),
                    ProductTypes = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCertificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceCertificate_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsuranceCertificate_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "InsurancesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsuranceCertificate_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsurancesCertificateLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsuraceCertificateId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancesCertificateLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsurancesCertificateLines_InsuranceCertificate_InsuraceCertificateId",
                        column: x => x.InsuraceCertificateId,
                        principalTable: "InsuranceCertificate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsurancesCertificateLines_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ProductTypeId",
                table: "Customer",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountTransfers_EstadosIdEstado",
                table: "BankAccountTransfers",
                column: "EstadosIdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceCertificate_CustomerId",
                table: "InsuranceCertificate",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceCertificate_InsuranceId",
                table: "InsuranceCertificate",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceCertificate_ProductTypeId",
                table: "InsuranceCertificate",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancesCertificateLines_InsuraceCertificateId",
                table: "InsurancesCertificateLines",
                column: "InsuraceCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancesCertificateLines_WarehouseId",
                table: "InsurancesCertificateLines",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccountTransfers_Estados_EstadosIdEstado",
                table: "BankAccountTransfers",
                column: "EstadosIdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_ProductType_ProductTypeId",
                table: "Customer",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "ProductTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccountTransfers_Estados_EstadosIdEstado",
                table: "BankAccountTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_ProductType_ProductTypeId",
                table: "Customer");

            migrationBuilder.DropTable(
                name: "InsurancesCertificateLines");

            migrationBuilder.DropTable(
                name: "InsuranceCertificate");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ProductTypeId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_BankAccountTransfers_EstadosIdEstado",
                table: "BankAccountTransfers");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "BankAccountTransfers");

            migrationBuilder.DropColumn(
                name: "EstadosIdEstado",
                table: "BankAccountTransfers");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedUser",
                table: "ExchangeRate",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUser",
                table: "ExchangeRate",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
