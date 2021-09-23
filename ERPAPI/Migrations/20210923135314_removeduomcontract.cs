using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class removeduomcontract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "CustomerContract");

            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasureId",
                table: "CustomerContractLines",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasureId",
                table: "CustomerContractLines",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasureId",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "CustomerContract",
                nullable: true);
        }
    }
}
