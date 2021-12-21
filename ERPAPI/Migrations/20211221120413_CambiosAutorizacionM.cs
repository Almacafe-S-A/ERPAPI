using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CambiosAutorizacionM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BankId",
                table: "GoodsDeliveryAuthorization",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BankId",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
