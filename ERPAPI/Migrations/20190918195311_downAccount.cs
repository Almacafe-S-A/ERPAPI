using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class downAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerLine_AccountingAccountId",
                table: "GeneralLedgerLine",
                column: "AccountingAccountId");
              */  
            /*migrationBuilder.CreateIndex(
                name: "IX_Account_AccountingAccountId",
                table: "Account",
                column: "AccountingAccountId");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.DropIndex(
                name: "IX_GeneralLedgerLine_AccountingAccountId",
                table: "GeneralLedgerLine");*/

           /* migrationBuilder.DropIndex(
                name: "IX_Account_AccountingAccountId",
                table: "Account");*/
        }
    }
}
