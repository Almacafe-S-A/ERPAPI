using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class goodsdelivered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoodsDelivered",
                columns: table => new
                {
                    GoodsDeliveredId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Currency = table.Column<double>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    WarehouseName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Reference = table.Column<string>(nullable: true),
                    ExitTicket = table.Column<long>(nullable: false),
                    Placa = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    WeightBallot = table.Column<long>(nullable: false),
                    PesoBruto = table.Column<double>(nullable: false),
                    TaraTransporte = table.Column<double>(nullable: false),
                    PesoNeto = table.Column<double>(nullable: false),
                    TaraUnidadMedida = table.Column<double>(nullable: false),
                    PesoNeto2 = table.Column<double>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsDelivered", x => x.GoodsDeliveredId);
                });

            migrationBuilder.CreateTable(
                name: "GoodsDeliveredLine",
                columns: table => new
                {
                    GoodsDeliveredLinedId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GoodsDeliveredId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    ProducId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ControlPalletsId = table.Column<long>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    QuantitySacos = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    CenterCostId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsDeliveredLine", x => x.GoodsDeliveredLinedId);
                    table.ForeignKey(
                        name: "FK_GoodsDeliveredLine_GoodsDelivered_GoodsDeliveredId",
                        column: x => x.GoodsDeliveredId,
                        principalTable: "GoodsDelivered",
                        principalColumn: "GoodsDeliveredId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveredLine_GoodsDeliveredId",
                table: "GoodsDeliveredLine",
                column: "GoodsDeliveredId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsDeliveredLine");

            migrationBuilder.DropTable(
                name: "GoodsDelivered");
        }
    }
}
