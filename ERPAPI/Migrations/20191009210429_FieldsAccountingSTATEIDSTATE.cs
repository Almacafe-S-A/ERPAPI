using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FieldsAccountingSTATEIDSTATE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Identidad",
                table: "Vendor",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CostCenterName",
                table: "CostCenter",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Accounting",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Accounting",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Accounting");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Accounting");

            migrationBuilder.AlterColumn<string>(
                name: "Identidad",
                table: "Vendor",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CostCenterName",
                table: "CostCenter",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
