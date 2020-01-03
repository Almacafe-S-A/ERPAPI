using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FixedAsset_Values : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AccumulatedDepreciation",
                table: "FixedAsset",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ActiveTotalCost",
                table: "FixedAsset",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NetValue",
                table: "FixedAsset",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ResidualValuePercent",
                table: "FixedAsset",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "AccumulatedDepreciation",
                table: "FixedAsset");

            migrationBuilder.DropColumn(
                name: "ActiveTotalCost",
                table: "FixedAsset");

            migrationBuilder.DropColumn(
                name: "NetValue",
                table: "FixedAsset");

            migrationBuilder.DropColumn(
                name: "ResidualValuePercent",
                table: "FixedAsset");
        }
    }
}
