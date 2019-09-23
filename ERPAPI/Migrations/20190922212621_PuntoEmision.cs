using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PuntoEmision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "PuntoEmision",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "PuntoEmision",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "NumeracionSAR",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "NumeracionSAR",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_cai",
                table: "NumeracionSAR",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "PuntoEmision");

            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "PuntoEmision");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "NumeracionSAR");

            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "NumeracionSAR");

            migrationBuilder.DropColumn(
                name: "_cai",
                table: "NumeracionSAR");
        }
    }
}
