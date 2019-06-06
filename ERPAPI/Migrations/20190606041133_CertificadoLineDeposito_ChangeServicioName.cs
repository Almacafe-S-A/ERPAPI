using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CertificadoLineDeposito_ChangeServicioName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ServicioName",
                table: "CertificadoDeposito",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ServicioName",
                table: "CertificadoDeposito",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
