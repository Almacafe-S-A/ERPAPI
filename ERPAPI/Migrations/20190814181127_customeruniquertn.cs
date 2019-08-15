using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class customeruniquertn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RTN",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Customer_RTN",
                table: "Customer",
                column: "RTN",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_RTN",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "RTN",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
