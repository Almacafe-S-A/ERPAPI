using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ProductRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
    

            migrationBuilder.CreateTable(
                name: "ProductRelation",
                columns: table => new
                {
                    RelationProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<long>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRelation", x => x.RelationProductId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductRelation");

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "SubProduct",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
