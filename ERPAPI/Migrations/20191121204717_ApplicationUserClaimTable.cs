using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ApplicationUserClaimTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             //migrationBuilder.DropForeignKey(
             //    name: "FK_EmployeeExtraHours_Customer_CustomerId",
             //    table: "EmployeeExtraHours");

             //migrationBuilder.AlterColumn<long>(
             //    name: "CustomerId",
             //    table: "EmployeeExtraHours",
             //    nullable: true,
             //    oldClrType: typeof(long));
                 
         /*   migrationBuilder.AddForeignKey(
                 name: "FK_EmployeeExtraHours_Customer_CustomerId",
                 table: "EmployeeExtraHours",
                 column: "CustomerId",
                 principalTable: "Customer",
                 principalColumn: "CustomerId",
                 onDelete: ReferentialAction.NoAction);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
             /* migrationBuilder.DropForeignKey(
                  name: "FK_EmployeeExtraHours_Customer_CustomerId",
                  table: "EmployeeExtraHours");*/

              migrationBuilder.AlterColumn<long>(
                  name: "CustomerId",
                  table: "EmployeeExtraHours",
                  nullable: false,
                  oldClrType: typeof(long),
                  oldNullable: true);

              /*migrationBuilder.AddForeignKey(
                  name: "FK_EmployeeExtraHours_Customer_CustomerId",
                  table: "EmployeeExtraHours",
                  column: "CustomerId",
                  principalTable: "Customer",
                  principalColumn: "CustomerId",
                  onDelete: ReferentialAction.NoAction);*/
        }
    }
}
