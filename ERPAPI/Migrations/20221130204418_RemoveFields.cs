using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RemoveFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CenterCostId",
                table: "CertificadoLine");

            migrationBuilder.DropColumn(
                name: "CenterCostName",
                table: "CertificadoLine");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CenterCostId",
                table: "CertificadoLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CenterCostName",
                table: "CertificadoLine",
                nullable: true);
        }
    }
}
