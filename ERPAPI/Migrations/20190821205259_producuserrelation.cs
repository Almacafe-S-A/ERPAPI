using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class producuserrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClaseInicial",
                table: "Bitacora",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocType",
                table: "Bitacora",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductUserRelation",
                columns: table => new
                {
                    ProductUserRelationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUserRelation", x => x.ProductUserRelationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductUserRelation");

            migrationBuilder.DropColumn(
                name: "ClaseInicial",
                table: "Bitacora");

            migrationBuilder.DropColumn(
                name: "DocType",
                table: "Bitacora");
        }
    }
}
