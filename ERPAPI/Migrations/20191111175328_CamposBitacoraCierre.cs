using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CamposBitacoraCierre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BitacoraCierreContableId",
                table: "CierresJournal",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BitacoraCierreContableId",
                table: "CierresAccounting",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CierresJournal_BitacoraCierreContableId",
                table: "CierresJournal",
                column: "BitacoraCierreContableId");

            migrationBuilder.CreateIndex(
                name: "IX_CierresAccounting_BitacoraCierreContableId",
                table: "CierresAccounting",
                column: "BitacoraCierreContableId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CierresAccounting_BitacoraCierreContable_BitacoraCierreContableId",
            //    table: "CierresAccounting",
            //    column: "BitacoraCierreContableId",
            //    principalTable: "BitacoraCierreContable",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CierresJournal_BitacoraCierreContable_BitacoraCierreContableId",
            //    table: "CierresJournal",
            //    column: "BitacoraCierreContableId",
            //    principalTable: "BitacoraCierreContable",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CierresAccounting_BitacoraCierreContable_BitacoraCierreContableId",
                table: "CierresAccounting");

            migrationBuilder.DropForeignKey(
                name: "FK_CierresJournal_BitacoraCierreContable_BitacoraCierreContableId",
                table: "CierresJournal");

            migrationBuilder.DropIndex(
                name: "IX_CierresJournal_BitacoraCierreContableId",
                table: "CierresJournal");

            migrationBuilder.DropIndex(
                name: "IX_CierresAccounting_BitacoraCierreContableId",
                table: "CierresAccounting");

            migrationBuilder.DropColumn(
                name: "BitacoraCierreContableId",
                table: "CierresJournal");

            migrationBuilder.DropColumn(
                name: "BitacoraCierreContableId",
                table: "CierresAccounting");
        }
    }
}
