using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Account_In_creditDebitLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "DebitNoteLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "DebitNoteLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "CreditNoteLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "CreditNoteLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "DebitNoteLine");

            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "DebitNoteLine");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "CreditNoteLine");

            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "CreditNoteLine");
        }
    }
}
