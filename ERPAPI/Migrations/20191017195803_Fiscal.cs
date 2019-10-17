using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Fiscal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BranchId",
                table: "ProformaInvoice",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "Fiscal",
                table: "DebitNote",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Fiscal",
                table: "CreditNote",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fiscal",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "Fiscal",
                table: "CreditNote");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "ProformaInvoice",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
