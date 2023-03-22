using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CompanyData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Saldo",
                table: "Invoice",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CodigoInstitucion",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoInstitucion",
                table: "CompanyInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CodigoInstitucion",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "TipoInstitucion",
                table: "CompanyInfo");
        }
    }
}
