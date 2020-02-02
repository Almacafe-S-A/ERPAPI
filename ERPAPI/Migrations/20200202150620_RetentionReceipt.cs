using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RetentionReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RetentionReceipt",
                columns: table => new
                {
                    RetentionReceiptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DueDate = table.Column<DateTime>(nullable: false),
                    DocumentId = table.Column<long>(nullable: false),
                    IdTipoDocumento = table.Column<long>(nullable: false),
                    NoCorrelativo = table.Column<string>(nullable: true),
                    CAI = table.Column<string>(nullable: true),
                    FechaEmision = table.Column<DateTime>(nullable: false),
                    RTN = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: true),
                    VendorId = table.Column<long>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetentionReceipt", x => x.RetentionReceiptId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RetentionReceipt");
        }
    }
}
