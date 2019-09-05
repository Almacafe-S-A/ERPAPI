using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class UnitOfMeasureUniqueWareHouseNam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UnitOfMeasureName",
                table: "UnitOfMeasure",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasure_UnitOfMeasureName",
                table: "UnitOfMeasure",
                column: "UnitOfMeasureName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UnitOfMeasure_UnitOfMeasureName",
                table: "UnitOfMeasure");

            migrationBuilder.AlterColumn<string>(
                name: "UnitOfMeasureName",
                table: "UnitOfMeasure",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
