using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class BlockedInJournalandQtymin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "QtyMin",
                table: "Purch",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "QtyMonth",
                table: "Purch",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "BlockedInJournal",
                table: "Account",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtyMin",
                table: "Purch");

            migrationBuilder.DropColumn(
                name: "QtyMonth",
                table: "Purch");

            migrationBuilder.DropColumn(
                name: "BlockedInJournal",
                table: "Account");
        }
    }
}
