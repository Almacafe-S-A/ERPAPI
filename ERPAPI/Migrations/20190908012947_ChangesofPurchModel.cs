using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ChangesofPurchModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "Purch");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Purch");

            migrationBuilder.DropColumn(
                name: "GrupoEconomico",
                table: "Purch");

            migrationBuilder.DropColumn(
                name: "Identidad",
                table: "Purch");

            migrationBuilder.DropColumn(
                name: "PurchCode",
                table: "Purch");

            migrationBuilder.DropColumn(
                name: "QtyMin",
                table: "Purch");

            migrationBuilder.DropColumn(
                name: "QtyMonth",
                table: "Purch");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                table: "Purch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Purch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GrupoEconomico",
                table: "Purch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identidad",
                table: "Purch",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PurchCode",
                table: "Purch",
                nullable: false,
                defaultValue: "");

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
    }
}
