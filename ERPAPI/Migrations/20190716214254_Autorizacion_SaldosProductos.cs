using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Autorizacion_SaldosProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BagBalance",
                table: "SubProduct",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "SubProduct",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "DelegadoFiscal",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DerechoLps",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "NoPoliza",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "CustomerProduct",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SaldoProductoSacos",
                table: "CustomerProduct",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "CompanyInfo",
                columns: table => new
                {
                    CompanyInfoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Tax_Id = table.Column<string>(nullable: true),
                    image = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInfo", x => x.CompanyInfoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "BagBalance",
                table: "SubProduct");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "SubProduct");

            migrationBuilder.DropColumn(
                name: "DelegadoFiscal",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "DerechoLps",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "NoPoliza",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "CustomerProduct");

            migrationBuilder.DropColumn(
                name: "SaldoProductoSacos",
                table: "CustomerProduct");
        }
    }
}
