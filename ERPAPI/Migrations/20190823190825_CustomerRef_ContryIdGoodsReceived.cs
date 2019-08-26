using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerRef_ContryIdGoodsReceived : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerRefNumber",
                table: "Customer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "CustomerRefNumber",
                table: "Customer");
        }
    }
}
