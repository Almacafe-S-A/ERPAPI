using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CreditNotefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caja",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "CustomerRefNumber",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "Freight",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "IdPuntoEmision",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "Tax18",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "TotalGravado18",
                table: "CreditNote");

            migrationBuilder.RenameColumn(
                name: "NumeroSAR",
                table: "CreditNote",
                newName: "NumeroDEIDocumentoAsociado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumeroDEIDocumentoAsociado",
                table: "CreditNote",
                newName: "NumeroSAR");

            migrationBuilder.AddColumn<string>(
                name: "Caja",
                table: "CreditNote",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Currency",
                table: "CreditNote",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "CreditNote",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "CreditNote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerRefNumber",
                table: "CreditNote",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Freight",
                table: "CreditNote",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "IdPuntoEmision",
                table: "CreditNote",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax18",
                table: "CreditNote",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalGravado18",
                table: "CreditNote",
                type: "Money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
