using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Contrato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "CompanyInfo",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manager",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrintHeader",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RTNMANAGER",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevOffice",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerContract",
                columns: table => new
                {
                    CustomerContractId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerManager = table.Column<string>(nullable: true),
                    RTNCustomerManager = table.Column<string>(nullable: true),
                    CustomerConstitution = table.Column<string>(nullable: true),
                    SalesOrderId = table.Column<long>(nullable: false),
                    Manager = table.Column<string>(nullable: true),
                    RTNMANAGER = table.Column<string>(nullable: true),
                    StorageTime = table.Column<string>(nullable: true),
                    LatePayment = table.Column<double>(nullable: false),
                    UsedArea = table.Column<double>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Reception = table.Column<string>(nullable: true),
                    Correo1 = table.Column<string>(nullable: true),
                    Correo2 = table.Column<string>(nullable: true),
                    Correo3 = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContract", x => x.CustomerContractId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContractWareHouse",
                columns: table => new
                {
                    CustomerContractWareHouseId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerContractId = table.Column<long>(nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    EdificioName = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContractWareHouse", x => x.CustomerContractWareHouseId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerContract");

            migrationBuilder.DropTable(
                name: "CustomerContractWareHouse");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "Manager",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "PrintHeader",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "RTNMANAGER",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "RevOffice",
                table: "CompanyInfo");
        }
    }
}
