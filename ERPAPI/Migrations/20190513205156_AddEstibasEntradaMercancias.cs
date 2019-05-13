using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddEstibasEntradaMercancias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CustomerConditions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ControlPallets",
                columns: table => new
                {
                    ControlPalletsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Motorista = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<int>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    DescriptionProduct = table.Column<string>(nullable: true),
                    Placa = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    PalletId = table.Column<int>(nullable: false),
                    EsIngreso = table.Column<int>(nullable: false),
                    EsSalida = table.Column<int>(nullable: false),
                    SubTotal = table.Column<int>(nullable: false),
                    TotalSacos = table.Column<int>(nullable: false),
                    SacosDevueltos = table.Column<int>(nullable: false),
                    QQPesoBruto = table.Column<double>(nullable: false),
                    Tara = table.Column<double>(nullable: false),
                    QQPesoNeto = table.Column<double>(nullable: false),
                    QQPesoFinal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlPallets", x => x.ControlPalletsId);
                });

            migrationBuilder.CreateTable(
                name: "ControlPalletsLine",
                columns: table => new
                {
                    ControlPalletsLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ControlPalletsId = table.Column<long>(nullable: false),
                    Alto = table.Column<int>(nullable: false),
                    Ancho = table.Column<int>(nullable: false),
                    Otros = table.Column<int>(nullable: false),
                    Totallinea = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlPalletsLine", x => x.ControlPalletsLineId);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceived",
                columns: table => new
                {
                    GoodsReceivedId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Reference = table.Column<string>(nullable: true),
                    ExitTicket = table.Column<long>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceived", x => x.GoodsReceivedId);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceivedLine",
                columns: table => new
                {
                    GoodsReceiveLinedId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GoodsReceivedId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    ProducId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ControlPalletsId = table.Column<long>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    QuantitySacos = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    CenterCostId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceivedLine", x => x.GoodsReceiveLinedId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlPallets");

            migrationBuilder.DropTable(
                name: "ControlPalletsLine");

            migrationBuilder.DropTable(
                name: "GoodsReceived");

            migrationBuilder.DropTable(
                name: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CustomerConditions");
        }
    }
}
