using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Added_FieldsCheckAccountLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "CheckAccountLines",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "NumeroActual",
                table: "CheckAccount",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CheckAccountLines_IdEstado",
                table: "CheckAccountLines",
                column: "IdEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckAccountLines_Estados_IdEstado",
                table: "CheckAccountLines",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckAccountLines_Estados_IdEstado",
                table: "CheckAccountLines");

            migrationBuilder.DropIndex(
                name: "IX_CheckAccountLines_IdEstado",
                table: "CheckAccountLines");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "CheckAccountLines");

            migrationBuilder.DropColumn(
                name: "NumeroActual",
                table: "CheckAccount");
        }
    }
}
