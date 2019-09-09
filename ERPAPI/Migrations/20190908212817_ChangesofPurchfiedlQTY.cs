using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ChangesofPurchfiedlQTY : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtyMin",
                table: "Purch");

            migrationBuilder.DropColumn(
                name: "QtyMonth",
                table: "Purch");

           
        }
    }
}
