using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class controlpalletsdetailnulls2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlPallets_UnitOfMeasure_UnitOfMeasureId",
                table: "ControlPallets");

            migrationBuilder.AlterColumn<int>(
                name: "UnitOfMeasureId",
                table: "ControlPallets",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPallets_UnitOfMeasure_UnitOfMeasureId",
                table: "ControlPallets",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlPallets_UnitOfMeasure_UnitOfMeasureId",
                table: "ControlPallets");

            migrationBuilder.AlterColumn<int>(
                name: "UnitOfMeasureId",
                table: "ControlPallets",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPallets_UnitOfMeasure_UnitOfMeasureId",
                table: "ControlPallets",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
