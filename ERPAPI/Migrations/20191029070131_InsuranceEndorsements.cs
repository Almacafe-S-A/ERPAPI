using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InsuranceEndorsements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsuranceEndorsement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    Customername = table.Column<string>(nullable: true),
                    WarehouseID = table.Column<int>(nullable: false),
                    WarehouseName = table.Column<string>(nullable: true),
                    WharehouseTypeId = table.Column<int>(nullable: false),
                    WarehouseTypeName = table.Column<string>(nullable: true),
                    DateGenerated = table.Column<DateTime>(nullable: false),
                    InsurancePolicyId = table.Column<int>(nullable: false),
                    ProductdId = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    TotalAmountLp = table.Column<double>(nullable: false),
                    TotalAmountDl = table.Column<double>(nullable: false),
                    CertificateBalalnce = table.Column<double>(nullable: false),
                    AssuredDifernce = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceEndorsement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceEndorsementLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsuranceEndorsementId = table.Column<int>(nullable: false),
                    WarehouseName = table.Column<string>(nullable: true),
                    WareHouseId = table.Column<int>(nullable: false),
                    AmountLp = table.Column<double>(nullable: false),
                    AmountDl = table.Column<double>(nullable: false),
                    CertificateBalance = table.Column<double>(nullable: false),
                    AssuredDiference = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceEndorsementLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceEndorsementLine_InsuranceEndorsement_InsuranceEndorsementId",
                        column: x => x.InsuranceEndorsementId,
                        principalTable: "InsuranceEndorsement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEndorsementLine_InsuranceEndorsementId",
                table: "InsuranceEndorsementLine",
                column: "InsuranceEndorsementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsuranceEndorsementLine");

            migrationBuilder.DropTable(
                name: "InsuranceEndorsement");
        }
    }
}
