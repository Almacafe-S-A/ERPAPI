using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SubProduct_TipoProhibicion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TipoProhibidoId",
                table: "SubProduct",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "TipoProhibidoName",
                table: "SubProduct",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoProhibidoId",
                table: "SubProduct");

            migrationBuilder.DropColumn(
                name: "TipoProhibidoName",
                table: "SubProduct");
        }
    }
}
