using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddesEstadosCheckAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "CheckAccount",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "CheckAccount",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckAccount_IdEstado",
                table: "CheckAccount",
                column: "IdEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckAccount_Estados_IdEstado",
                table: "CheckAccount",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckAccount_Estados_IdEstado",
                table: "CheckAccount");

            migrationBuilder.DropIndex(
                name: "IX_CheckAccount_IdEstado",
                table: "CheckAccount");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "CheckAccount");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "CheckAccount");
        }
    }
}
