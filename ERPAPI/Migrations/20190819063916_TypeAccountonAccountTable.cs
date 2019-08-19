using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TypeAccountonAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockedInJournal",
                table: "Account");

            migrationBuilder.AddColumn<long>(
                name: "TypeAccountId",
                table: "Account",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeAccountId",
                table: "Account");

            migrationBuilder.AddColumn<bool>(
                name: "BlockedInJournal",
                table: "Account",
                nullable: false,
                defaultValue: false);
        }
    }
}
