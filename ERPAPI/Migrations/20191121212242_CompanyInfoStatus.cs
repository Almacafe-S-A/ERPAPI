using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CompanyInfoStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "CompanyInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "CompanyInfo");
        }
    }
}
