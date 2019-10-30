using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ConciliacionLinea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "ConciliacionLinea",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "ConciliacionLinea",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConciliacionId",
                table: "ConciliacionLinea",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Credit",
                table: "ConciliacionLinea",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Dedit",
                table: "ConciliacionLinea",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_ConciliacionLinea_ConciliacionId",
                table: "ConciliacionLinea",
                column: "ConciliacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConciliacionLinea_Conciliacion_ConciliacionId",
                table: "ConciliacionLinea",
                column: "ConciliacionId",
                principalTable: "Conciliacion",
                principalColumn: "ConciliacionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConciliacionLinea_Conciliacion_ConciliacionId",
                table: "ConciliacionLinea");

            migrationBuilder.DropIndex(
                name: "IX_ConciliacionLinea_ConciliacionId",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "ConciliacionId",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "Credit",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "Dedit",
                table: "ConciliacionLinea");
        }
    }
}
