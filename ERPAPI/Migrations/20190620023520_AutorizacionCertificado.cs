using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AutorizacionCertificado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoodsDeliveryAuthorization",
                columns: table => new
                {
                    GoodsDeliveryAuthorizationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorizationName = table.Column<string>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    AuthorizationDate = table.Column<DateTime>(nullable: false),
                    NoCD = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    TotalCertificado = table.Column<double>(nullable: false),
                    TotalFinanciado = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsDeliveryAuthorization", x => x.GoodsDeliveryAuthorizationId);
                });

            migrationBuilder.CreateTable(
                name: "GoodsDeliveryAuthorizationLine",
                columns: table => new
                {
                    GoodsDeliveryAuthorizationLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GoodsDeliveryAuthorizationId = table.Column<long>(nullable: false),
                    NoCertificadoDeposito = table.Column<long>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    valorcertificado = table.Column<double>(nullable: false),
                    valorfinanciado = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsDeliveryAuthorizationLine", x => x.GoodsDeliveryAuthorizationLineId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsDeliveryAuthorization");

            migrationBuilder.DropTable(
                name: "GoodsDeliveryAuthorizationLine");
        }
    }
}
