using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SujetasAPagpCDE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SujetasAPago",
                table: "CertificadoDeposito",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SujetasAPago",
                table: "CertificadoDeposito",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
