using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class addedfields_orderSAle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodoCobro",
                table: "SalesOrderLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "TipoCobroId",
                table: "SalesOrderLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_TipoCobroId",
                table: "SalesOrderLine",
                column: "TipoCobroId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_ElementoConfiguracion_TipoCobroId",
                table: "SalesOrderLine",
                column: "TipoCobroId",
                principalTable: "ElementoConfiguracion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_ElementoConfiguracion_TipoCobroId",
                table: "SalesOrderLine");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLine_TipoCobroId",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "PeriodoCobro",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "TipoCobroId",
                table: "SalesOrderLine");
        }
    }
}
