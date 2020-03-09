using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFields_CustomerContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observcion",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContract_IdEstado",
                table: "CustomerContract",
                column: "IdEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContract_Estados_IdEstado",
                table: "CustomerContract",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContract_Estados_IdEstado",
                table: "CustomerContract");

            migrationBuilder.DropIndex(
                name: "IX_CustomerContract_IdEstado",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Observcion",
                table: "CustomerContract");
        }
    }
}
