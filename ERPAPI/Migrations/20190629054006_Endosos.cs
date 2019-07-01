using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Endosos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EndososBono",
                columns: table => new
                {
                    EndososBonoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCD = table.Column<long>(nullable: false),
                    NoCD = table.Column<long>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    CantidadEndosar = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    ValorEndosar = table.Column<double>(nullable: false),
                    TipoEndosoId = table.Column<long>(nullable: false),
                    TipoEndosoName = table.Column<string>(nullable: true),
                    FirmadoEn = table.Column<string>(nullable: true),
                    TipoEndoso = table.Column<long>(nullable: false),
                    NombreEndoso = table.Column<string>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    FechaOtorgado = table.Column<DateTime>(nullable: false),
                    TasaDeInteres = table.Column<double>(nullable: false),
                    TotalEndoso = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososBono", x => x.EndososBonoId);
                });

            migrationBuilder.CreateTable(
                name: "EndososBonoLine",
                columns: table => new
                {
                    EndososBonoLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndososBonoId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    ValorEndoso = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososBonoLine", x => x.EndososBonoLineId);
                });

            migrationBuilder.CreateTable(
                name: "EndososCertificados",
                columns: table => new
                {
                    EndososCertificadosId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCD = table.Column<long>(nullable: false),
                    NoCD = table.Column<long>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    CantidadEndosar = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    ValorEndosar = table.Column<double>(nullable: false),
                    TipoEndosoId = table.Column<long>(nullable: false),
                    TipoEndosoName = table.Column<string>(nullable: true),
                    FirmadoEn = table.Column<string>(nullable: true),
                    TipoEndoso = table.Column<long>(nullable: false),
                    NombreEndoso = table.Column<string>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    FechaOtorgado = table.Column<DateTime>(nullable: false),
                    TasaDeInteres = table.Column<double>(nullable: false),
                    TotalEndoso = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososCertificados", x => x.EndososCertificadosId);
                });

            migrationBuilder.CreateTable(
                name: "EndososCertificadosLine",
                columns: table => new
                {
                    EndososCertificadosLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndososCertificadosId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    ValorEndoso = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososCertificadosLine", x => x.EndososCertificadosLineId);
                });

            migrationBuilder.CreateTable(
                name: "EndososLiberacion",
                columns: table => new
                {
                    EndososLiberacionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndososId = table.Column<long>(nullable: false),
                    EndososLineId = table.Column<long>(nullable: false),
                    TipoEndoso = table.Column<string>(nullable: true),
                    FechaLiberacion = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Saldo = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososLiberacion", x => x.EndososLiberacionId);
                });

            migrationBuilder.CreateTable(
                name: "EndososTalon",
                columns: table => new
                {
                    EndososTalonId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCD = table.Column<long>(nullable: false),
                    NoCD = table.Column<long>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    CantidadEndosar = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    ValorEndosar = table.Column<double>(nullable: false),
                    TipoEndosoId = table.Column<long>(nullable: false),
                    TipoEndosoName = table.Column<string>(nullable: true),
                    FirmadoEn = table.Column<string>(nullable: true),
                    TipoEndoso = table.Column<long>(nullable: false),
                    NombreEndoso = table.Column<string>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    FechaOtorgado = table.Column<DateTime>(nullable: false),
                    TasaDeInteres = table.Column<double>(nullable: false),
                    TotalEndoso = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososTalon", x => x.EndososTalonId);
                });

            migrationBuilder.CreateTable(
                name: "EndososTalonLine",
                columns: table => new
                {
                    EndososTalonLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndososTalonId = table.Column<long>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    ValorEndoso = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososTalonLine", x => x.EndososTalonLineId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndososBono");

            migrationBuilder.DropTable(
                name: "EndososBonoLine");

            migrationBuilder.DropTable(
                name: "EndososCertificados");

            migrationBuilder.DropTable(
                name: "EndososCertificadosLine");

            migrationBuilder.DropTable(
                name: "EndososLiberacion");

            migrationBuilder.DropTable(
                name: "EndososTalon");

            migrationBuilder.DropTable(
                name: "EndososTalonLine");
        }
    }
}
