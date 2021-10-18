using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AgregadasCamposContratos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reception",
                table: "CustomerContract",
                newName: "RelacionPartes");

            migrationBuilder.AddColumn<string>(
                name: "ContitucionAlmacafe",
                table: "CustomerContract",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContitucionAlmacafe",
                table: "CustomerContract");

            migrationBuilder.RenameColumn(
                name: "RelacionPartes",
                table: "CustomerContract",
                newName: "Reception");
        }
    }
}
