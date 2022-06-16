using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class BoletasPeso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PesoTMI",
                table: "Boleto_Ent",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "PesoTM",
                table: "Boleto_Ent",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "PlacaContenedor",
                table: "Boleto_Ent",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlacaContenedor",
                table: "Boleto_Ent");

            migrationBuilder.AlterColumn<decimal>(
                name: "PesoTMI",
                table: "Boleto_Ent",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "PesoTM",
                table: "Boleto_Ent",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money");
        }
    }
}
