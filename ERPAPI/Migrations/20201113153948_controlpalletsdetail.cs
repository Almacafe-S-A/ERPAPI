using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class controlpalletsdetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubProductName",
                table: "ControlPalletsLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitofMeasureName",
                table: "ControlPalletsLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseName",
                table: "ControlPalletsLine",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "WeightBallot",
                table: "ControlPallets",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubProductName",
                table: "ControlPalletsLine");

            migrationBuilder.DropColumn(
                name: "UnitofMeasureName",
                table: "ControlPalletsLine");

            migrationBuilder.DropColumn(
                name: "WarehouseName",
                table: "ControlPalletsLine");

            migrationBuilder.AlterColumn<long>(
                name: "WeightBallot",
                table: "ControlPallets",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
