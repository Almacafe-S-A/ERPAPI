using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class NotaCredito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroSAR",
                table: "CreditNote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RangoAutorizado",
                table: "CreditNote",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroSAR",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "RangoAutorizado",
                table: "CreditNote");
        }
    }
}
