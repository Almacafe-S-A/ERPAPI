using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PurchDocuments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchDocument",
                columns: table => new
                {
                    PurchDocumentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PurchId = table.Column<long>(nullable: false),
                    DocumentTypeId = table.Column<long>(nullable: false),
                    DocumentTypeName = table.Column<string>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchDocument", x => x.PurchDocumentId);
                });

            migrationBuilder.CreateTable(
                name: "PurchPartners",
                columns: table => new
                {
                    PartnerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PartnerName = table.Column<string>(nullable: true),
                    PurchId = table.Column<long>(nullable: false),
                    Identidad = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Listados = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchPartners", x => x.PartnerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchDocument");

            migrationBuilder.DropTable(
                name: "PurchPartners");
        }
    }
}
