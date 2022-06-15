using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddFieldsBoletaPesol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DestinoI",
                table: "Boleto_Ent",
                newName: "Destino");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Destino",
                table: "Boleto_Ent",
                newName: "DestinoI");
        }
    }
}
