using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CostCenterBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "CostCenter",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "CostCenter",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "CostCenter",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "CostCenter",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "CostCenter");
        }
    }
}
