using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class UnidadLetrasAutorizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TotalUnidadesLetras" +
                "",
                table: "GoodsDeliveryAuthorization",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalUnidadesLetras",
                table: "GoodsDeliveryAuthorization");
        }
    }
}
