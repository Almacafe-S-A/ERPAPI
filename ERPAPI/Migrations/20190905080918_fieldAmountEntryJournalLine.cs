using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class fieldAmountEntryJournalLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "JournalEntryLine",
                nullable: false,
                oldClrType: typeof(decimal));

           

            

           


         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          /*  migrationBuilder.DropColumn(
                name: "CityId",
                table: "Customer");

           
            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Branch");
                */
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "JournalEntryLine",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
