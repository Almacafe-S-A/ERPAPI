using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class EliminarTablaAutorizacionesRecibos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsDeliveryAuthLines");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "GoodsDeliveredLine");

            migrationBuilder.DropColumn(
                name: "ProducId",
                table: "GoodsDeliveredLine");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "GoodsDeliveredLine");

            migrationBuilder.AddColumn<long>(
                name: "GoodsDeliveredId",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NoARLine",
                table: "GoodsDeliveredLine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoARLineId",
                table: "GoodsDeliveredLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveryAuthorization_GoodsDeliveredId",
                table: "GoodsDeliveryAuthorization",
                column: "GoodsDeliveredId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveredLine_NoARLine",
                table: "GoodsDeliveredLine",
                column: "NoARLine");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsDeliveredLine_GoodsDeliveryAuthorizationLine_NoARLine",
                table: "GoodsDeliveredLine",
                column: "NoARLine",
                principalTable: "GoodsDeliveryAuthorizationLine",
                principalColumn: "GoodsDeliveryAuthorizationLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsDeliveryAuthorization_GoodsDelivered_GoodsDeliveredId",
                table: "GoodsDeliveryAuthorization",
                column: "GoodsDeliveredId",
                principalTable: "GoodsDelivered",
                principalColumn: "GoodsDeliveredId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsDeliveredLine_GoodsDeliveryAuthorizationLine_NoARLine",
                table: "GoodsDeliveredLine");

            migrationBuilder.DropForeignKey(
                name: "FK_GoodsDeliveryAuthorization_GoodsDelivered_GoodsDeliveredId",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropIndex(
                name: "IX_GoodsDeliveryAuthorization_GoodsDeliveredId",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropIndex(
                name: "IX_GoodsDeliveredLine_NoARLine",
                table: "GoodsDeliveredLine");

            migrationBuilder.DropColumn(
                name: "GoodsDeliveredId",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "NoARLine",
                table: "GoodsDeliveredLine");

            migrationBuilder.DropColumn(
                name: "NoARLineId",
                table: "GoodsDeliveredLine");

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "GoodsDeliveredLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProducId",
                table: "GoodsDeliveredLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "GoodsDeliveredLine",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GoodsDeliveryAuthLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<decimal>(nullable: false),
                    GoodsAuhorizationLineId = table.Column<int>(nullable: false),
                    GoodsDeliveredLinedId = table.Column<int>(nullable: false),
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
                name: "IX_GoodsDeliveryAuthLines_GoodsDeliveredLinedId1",
                table: "GoodsDeliveryAuthLines",
                column: "GoodsDeliveredLinedId1");
        }
    }
}
