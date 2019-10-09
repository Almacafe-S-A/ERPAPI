using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DeletePurch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purch");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Purch",
                columns: table => new
                {
                    PurchId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CompanyReferenceone = table.Column<string>(nullable: false),
                    CompanyReferencetwo = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Identidad = table.Column<string>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    PhoneReferenceone = table.Column<string>(nullable: true),
                    PhoneReferencetwo = table.Column<string>(nullable: true),
                    PurchName = table.Column<string>(nullable: false),
                    PurchTypeId = table.Column<int>(nullable: false),
                    QtyMin = table.Column<double>(nullable: false),
                    QtyMonth = table.Column<double>(nullable: false),
                    RTN = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    taxGroup = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purch", x => x.PurchId);
                });
        }
    }
}
