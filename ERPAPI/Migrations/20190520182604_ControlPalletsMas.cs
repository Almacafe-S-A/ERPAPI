using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ControlPalletsMas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductTypeId",
                table: "SubProduct",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeName",
                table: "SubProduct",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "ControlPallets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "ControlPallets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SubProductId",
                table: "ControlPallets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerProduct",
                columns: table => new
                {
                    CustomerProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProduct", x => x.CustomerProductId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerProduct");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "SubProduct");

            migrationBuilder.DropColumn(
                name: "ProductTypeName",
                table: "SubProduct");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "ControlPallets");
        }
    }
}
