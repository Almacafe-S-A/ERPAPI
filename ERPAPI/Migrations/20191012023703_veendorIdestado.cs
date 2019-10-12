using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class veendorIdestado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EstadoVendorConfi",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstadoVendorConfi",
                table: "Vendor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoVendorConfi",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "IdEstadoVendorConfi",
                table: "Vendor");
        }
    }
}
