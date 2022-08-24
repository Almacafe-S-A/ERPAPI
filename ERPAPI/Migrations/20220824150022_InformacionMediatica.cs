using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InformacionMediatica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "BlackListCustomers",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "BlackListCustomers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "BlackListCustomers");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "BlackListCustomers",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
