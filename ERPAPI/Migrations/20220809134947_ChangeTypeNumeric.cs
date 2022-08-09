using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ChangeTypeNumeric : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SujetasAPago",
                table: "CertificadoDeposito",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SujetasAPago",
                table: "CertificadoDeposito",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
