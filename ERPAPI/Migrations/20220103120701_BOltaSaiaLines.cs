using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class BOltaSaiaLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentLine",
                table: "Kardex",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BoletaDeSalidaLines",
                columns: table => new
                {
                    BoletaSalidaLineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: true),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    BoletaDeSalidaId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoletaDeSalidaLines", x => x.BoletaSalidaLineId);
                    table.ForeignKey(
                        name: "FK_BoletaDeSalidaLines_BoletaDeSalida_BoletaDeSalidaId",
                        column: x => x.BoletaDeSalidaId,
                        principalTable: "BoletaDeSalida",
                        principalColumn: "BoletaDeSalidaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodsDeliveryAuthLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GoodsDeliveredLinedId = table.Column<int>(nullable: false),
                    GoodsAuhorizationLineId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<decimal>(nullable: false),
                    GoodsDeliveredLinedId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsDeliveryAuthLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsDeliveryAuthLines_GoodsDeliveredLine_GoodsDeliveredLinedId1",
                        column: x => x.GoodsDeliveredLinedId1,
                        principalTable: "GoodsDeliveredLine",
                        principalColumn: "GoodsDeliveredLinedId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoletaDeSalidaLines_BoletaDeSalidaId",
                table: "BoletaDeSalidaLines",
                column: "BoletaDeSalidaId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveryAuthLines_GoodsDeliveredLinedId1",
                table: "GoodsDeliveryAuthLines",
                column: "GoodsDeliveredLinedId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoletaDeSalidaLines");

            migrationBuilder.DropTable(
                name: "GoodsDeliveryAuthLines");

            migrationBuilder.DropColumn(
                name: "DocumentLine",
                table: "Kardex");
        }
    }
}
