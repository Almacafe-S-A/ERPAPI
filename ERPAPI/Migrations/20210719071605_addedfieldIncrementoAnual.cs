using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class addedfieldIncrementoAnual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoCobroName",
                table: "SalesOrderLine",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IncrementoAnual",
                table: "SalesOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoCobroName",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "IncrementoAnual",
                table: "SalesOrder");
        }
    }
}
