using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class EstadoCD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "IdEstado",
                table: "CertificadoDeposito",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "IdEstado",
                table: "CertificadoDeposito",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
