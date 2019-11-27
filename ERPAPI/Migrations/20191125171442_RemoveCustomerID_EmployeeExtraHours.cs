using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RemoveCustomerID_EmployeeExtraHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_EmployeeExtraHours_Customer_CustomerId",
            //    table: "EmployeeExtraHours");

            //migrationBuilder.DropIndex(
            //    name: "IX_EmployeeExtraHours_CustomerId",
            //    table: "EmployeeExtraHours");

            //migrationBuilder.DropColumn(                  ERROR AL GENERAR BASE DE DATOS NUEVA        
            //    name: "CustomerId",
            //    table: "EmployeeExtraHours");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<long>(
            //    name: "CustomerId",
            //    table: "EmployeeExtraHours",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_EmployeeExtraHours_CustomerId",
            //    table: "EmployeeExtraHours",
            //    column: "CustomerId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EmployeeExtraHours_Customer_CustomerId",
            //    table: "EmployeeExtraHours",
            //    column: "CustomerId",
            //    principalTable: "Customer",
            //    principalColumn: "CustomerId",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
