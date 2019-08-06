using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class valorimpuestos_CDAuthorization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ValorImpuestos",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorImpuestos",
                table: "CertificadoLine",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorImpuestos",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropColumn(
                name: "ValorImpuestos",
                table: "CertificadoLine");
        }
    }
}