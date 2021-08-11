using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class removefkweigthballot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlPallets_Boleto_Ent_WeightBallot",
                table: "ControlPallets");

            migrationBuilder.DropIndex(
                name: "IX_ControlPallets_WeightBallot",
                table: "ControlPallets");

            migrationBuilder.AlterColumn<long>(
                name: "WeightBallot",
                table: "ControlPallets",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "WeightBallot",
                table: "ControlPallets",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_ControlPallets_WeightBallot",
                table: "ControlPallets",
                column: "WeightBallot");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPallets_Boleto_Ent_WeightBallot",
                table: "ControlPallets",
                column: "WeightBallot",
                principalTable: "Boleto_Ent",
                principalColumn: "clave_e",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
