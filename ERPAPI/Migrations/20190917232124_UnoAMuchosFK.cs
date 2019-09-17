using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class UnoAMuchosFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vendor_StateId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Product_CurrencyId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdCity",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdCountry",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdCurrency",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdEstado",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdState",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Customer_StateId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Branch_StateId",
                table: "Branch");

            migrationBuilder.AlterColumn<long>(
                name: "StateId",
                table: "Branch",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_StateId",
                table: "Vendor",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CurrencyId",
                table: "Product",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdCity",
                table: "Employees",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdCountry",
                table: "Employees",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdCurrency",
                table: "Employees",
                column: "IdCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdEstado",
                table: "Employees",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdState",
                table: "Employees",
                column: "IdState");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_StateId",
                table: "Customer",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_StateId",
                table: "Branch",
                column: "StateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vendor_StateId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Product_CurrencyId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdCity",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdCountry",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdCurrency",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdEstado",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdState",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Customer_StateId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Branch_StateId",
                table: "Branch");

            migrationBuilder.AlterColumn<long>(
                name: "StateId",
                table: "Branch",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_StateId",
                table: "Vendor",
                column: "StateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CurrencyId",
                table: "Product",
                column: "CurrencyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdCity",
                table: "Employees",
                column: "IdCity",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdCountry",
                table: "Employees",
                column: "IdCountry",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdCurrency",
                table: "Employees",
                column: "IdCurrency",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdEstado",
                table: "Employees",
                column: "IdEstado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdState",
                table: "Employees",
                column: "IdState",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_StateId",
                table: "Customer",
                column: "StateId",
                unique: true,
                filter: "[StateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_StateId",
                table: "Branch",
                column: "StateId",
                unique: true,
                filter: "[StateId] IS NOT NULL");
        }
    }
}
