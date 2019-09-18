using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class EmployeeDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeDocument",
                columns: table => new
                {
                    EmployeeDocumentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<long>(nullable: false),
                    DocumentTypeId = table.Column<long>(nullable: false),
                    DocumentTypeName = table.Column<string>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    FechaIngreso = table.Column<DateTime>(nullable: false),
                    FechaVencimiento = table.Column<DateTime>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDocument", x => x.EmployeeDocumentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeDocument");
        }
    }
}
