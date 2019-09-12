using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InsurancesEmployeeSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyReferenceone",
                table: "Vendor",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyReferencetwo",
                table: "Vendor",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Vendor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Vendor",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Identidad",
                table: "Vendor",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneReferenceone",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneReferencetwo",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QtyMin",
                table: "Vendor",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "QtyMonth",
                table: "Vendor",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RTN",
                table: "Vendor",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "LimitCNBS",
                table: "Branch",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConfigurationVendor",
                columns: table => new
                {
                    ConfigurationVendorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QtyMin = table.Column<double>(nullable: false),
                    QtyMonth = table.Column<double>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationVendor", x => x.ConfigurationVendorId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalary",
                columns: table => new
                {
                    EmployeeSalaryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<long>(nullable: false),
                    QtySalary = table.Column<decimal>(nullable: false),
                    IdFrequency = table.Column<long>(nullable: false),
                    DayApplication = table.Column<DateTime>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalary", x => x.EmployeeSalaryId);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    InsurancesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsurancesName = table.Column<string>(nullable: true),
                    PhotoInsurances = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.InsurancesId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationVendor");

            migrationBuilder.DropTable(
                name: "EmployeeSalary");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropColumn(
                name: "CompanyReferenceone",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "CompanyReferencetwo",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "Identidad",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "PhoneReferenceone",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "PhoneReferencetwo",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "QtyMin",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "QtyMonth",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "RTN",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "LimitCNBS",
                table: "Branch");
        }
    }
}
