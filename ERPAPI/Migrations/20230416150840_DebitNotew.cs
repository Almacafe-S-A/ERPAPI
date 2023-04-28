using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DebitNotew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReangoAutorizado",
                table: "DebitNote",
                newName: "RangoAutorizado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RangoAutorizado",
                table: "DebitNote",
                newName: "ReangoAutorizado");
        }
    }
}
