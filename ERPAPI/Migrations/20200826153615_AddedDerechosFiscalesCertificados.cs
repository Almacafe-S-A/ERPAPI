using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedDerechosFiscalesCertificados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DerechosFiscales",
                table: "CertificadoLine",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<int>(
                name: "PdaNo",
                table: "CertificadoLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdaNo",
                table: "CertificadoLine");

            migrationBuilder.AlterColumn<decimal>(
                name: "DerechosFiscales",
                table: "CertificadoLine",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
