using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddFieldsInsurancesCertificateRates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "InsurancesCertificate");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "InsurancesCertificate");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "InsurancesCertificate");

            migrationBuilder.DropColumn(
                name: "FechaFirma",
                table: "InsurancesCertificate");

            migrationBuilder.DropColumn(
                name: "GrupoEconomicoId",
                table: "InsurancesCertificate");

            migrationBuilder.DropColumn(
                name: "LugarFirma",
                table: "InsurancesCertificate");

            migrationBuilder.DropColumn(
                name: "TotalLetras",
                table: "InsurancesCertificate");

            migrationBuilder.AddColumn<decimal>(
                name: "DifferenceTotalofProductInsurance",
                table: "InsurancesCertificate",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "QtyofMonths",
                table: "InsurancesCertificate",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RateInsurance",
                table: "InsurancesCertificate",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Ratedeductible",
                table: "InsurancesCertificate",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RateofProduct",
                table: "InsurancesCertificate",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalInsurancesofProduct",
                table: "InsurancesCertificate",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Totaldeductible",
                table: "InsurancesCertificate",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotaldeductibleofProduct",
                table: "InsurancesCertificate",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalofProduct",
                table: "InsurancesCertificate",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DifferenceTotalofProductInsurance",
                table: "InsurancesCertificate");

            migrationBuilder.DropColumn(
                name: "QtyofMonths",
                table: "InsurancesCertificate");

            migrationBuilder.DropColumn(
                name: "RateInsurance",
                table: "InsurancesCertificate");

            migrationBuilder.DropColumn(
                name: "Ratedeductible",
                table: "InsurancesCertificate");

            migrationBuilder.DropColumn(
                name: "RateofProduct",
                table: "InsurancesCertificate");

            migrationBuilder.DropColumn(
                name: "TotalInsurancesofProduct",
                table: "InsurancesCertificate");

            migrationBuilder.DropColumn(
                name: "Totaldeductible",
                table: "InsurancesCertificate");

            migrationBuilder.DropColumn(
                name: "TotaldeductibleofProduct",
                table: "InsurancesCertificate");

            migrationBuilder.DropColumn(
                name: "TotalofProduct",
                table: "InsurancesCertificate");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "InsurancesCertificate",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "InsurancesCertificate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "InsurancesCertificate",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFirma",
                table: "InsurancesCertificate",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "GrupoEconomicoId",
                table: "InsurancesCertificate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LugarFirma",
                table: "InsurancesCertificate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalLetras",
                table: "InsurancesCertificate",
                nullable: true);
        }
    }
}
