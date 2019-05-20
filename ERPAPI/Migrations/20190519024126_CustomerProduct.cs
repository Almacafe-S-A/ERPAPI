using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductTypeId",
                table: "SubProduct",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeName",
                table: "SubProduct",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SubProductId",
                table: "ControlPallets",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "SubProduct");

            migrationBuilder.DropColumn(
                name: "ProductTypeName",
                table: "SubProduct");

            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "ControlPallets");
        }
    }
}
