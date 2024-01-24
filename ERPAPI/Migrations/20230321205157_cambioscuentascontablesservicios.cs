using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class cambioscuentascontablesservicios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CuentaImpuestoporPagarNombre",
                table: "Tax",
                newName: "CuentaContablePorCobrarNombre");

            migrationBuilder.RenameColumn(
                name: "CuentaImpuestoporCobrarNombre",
                table: "Tax",
                newName: "CuentaContableIngresosNombre");

            migrationBuilder.RenameColumn(
                name: "AccountNamePorCobrar",
                table: "ProductRelation",
                newName: "CuentaContablePorCobrarNombre");

            migrationBuilder.RenameColumn(
                name: "AccountName",
                table: "ProductRelation",
                newName: "CuentaContableIngresosNombre");

            migrationBuilder.AddColumn<long>(
                name: "CuentaContableIngresosId",
                table: "Tax",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CuentaContablePorCobrarId",
                table: "Tax",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CuentaContableIdPorCobrar",
                table: "ProductRelation",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CuentaContableIngresosId",
                table: "ProductRelation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CuentaContableIngresosId",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "CuentaContablePorCobrarId",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "CuentaContableIdPorCobrar",
                table: "ProductRelation");

            migrationBuilder.DropColumn(
                name: "CuentaContableIngresosId",
                table: "ProductRelation");

            migrationBuilder.RenameColumn(
                name: "CuentaContablePorCobrarNombre",
                table: "Tax",
                newName: "CuentaImpuestoporPagarNombre");

            migrationBuilder.RenameColumn(
                name: "CuentaContableIngresosNombre",
                table: "Tax",
                newName: "CuentaImpuestoporCobrarNombre");

            migrationBuilder.RenameColumn(
                name: "CuentaContablePorCobrarNombre",
                table: "ProductRelation",
                newName: "AccountNamePorCobrar");

            migrationBuilder.RenameColumn(
                name: "CuentaContableIngresosNombre",
                table: "ProductRelation",
                newName: "AccountName");
        }
    }
}
