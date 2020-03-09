using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedSubserviceToDebitNoteCreditnote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SubProductId",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubProductName",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SubProductId",
                table: "CreditNote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubProductName",
                table: "CreditNote",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "SubProductName",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "SubProductName",
                table: "CreditNote");
        }
    }
}
