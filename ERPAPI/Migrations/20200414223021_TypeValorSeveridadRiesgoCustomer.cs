using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TypeValorSeveridadRiesgoCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ValorSeveridadRiesgo",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ValorSeveridadRiesgo",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
