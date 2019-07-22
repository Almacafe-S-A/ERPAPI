using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class customerAuthorizedSignatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CustomerArea");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "CustomerArea");

            migrationBuilder.CreateTable(
                name: "CustomerAreaProduct",
                columns: table => new
                {
                    CustomerAreaProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerAreaId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaMoficacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAreaProduct", x => x.CustomerAreaProductId);
                    table.ForeignKey(
                        name: "FK_CustomerAreaProduct_CustomerArea_CustomerAreaId",
                        column: x => x.CustomerAreaId,
                        principalTable: "CustomerArea",
                        principalColumn: "CustomerAreaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAuthorizedSignature",
                columns: table => new
                {
                    CustomerAuthorizedSignatureId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAuthorizedSignature", x => x.CustomerAuthorizedSignatureId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAreaProduct_CustomerAreaId",
                table: "CustomerAreaProduct",
                column: "CustomerAreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAreaProduct");

            migrationBuilder.DropTable(
                name: "CustomerAuthorizedSignature");

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "CustomerArea",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "CustomerArea",
                nullable: true);
        }
    }
}
