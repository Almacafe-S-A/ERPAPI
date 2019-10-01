using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CostCenterId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CenterCostName",
                table: "KardexLine",
                newName: "CostCenterName");

            migrationBuilder.RenameColumn(
                name: "CenterCostId",
                table: "KardexLine",
                newName: "CostCenterId");

            migrationBuilder.RenameColumn(
                name: "CenterCostId",
                table: "GoodsReceivedLine",
                newName: "CostCenterId");

            migrationBuilder.RenameColumn(
                name: "CenterCostId",
                table: "GoodsDeliveredLine",
                newName: "CostCenterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CostCenterName",
                table: "KardexLine",
                newName: "CenterCostName");

            migrationBuilder.RenameColumn(
                name: "CostCenterId",
                table: "KardexLine",
                newName: "CenterCostId");

            migrationBuilder.RenameColumn(
                name: "CostCenterId",
                table: "GoodsReceivedLine",
                newName: "CenterCostId");

            migrationBuilder.RenameColumn(
                name: "CostCenterId",
                table: "GoodsDeliveredLine",
                newName: "CenterCostId");
        }
    }
}
