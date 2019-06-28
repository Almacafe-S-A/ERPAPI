using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerArea",
                columns: table => new
                {
                    CustomerAreaId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    TypeId = table.Column<long>(nullable: false),
                    TypeName = table.Column<string>(nullable: true),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    Ancho = table.Column<double>(nullable: false),
                    Alto = table.Column<double>(nullable: false),
                    Largo = table.Column<double>(nullable: false),
                    UsedArea = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerArea", x => x.CustomerAreaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerArea");
        }
    }
}
