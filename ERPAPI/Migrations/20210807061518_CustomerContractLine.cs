using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerContractLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerContractLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerContractId = table.Column<long>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Valor = table.Column<decimal>(nullable: true),
                    Porcentaje = table.Column<decimal>(nullable: true),
                    TipoCobroId = table.Column<long>(nullable: true),
                    TipoCobroName = table.Column<string>(nullable: true),
                    PeriodoCobro = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContractLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContractLines_CustomerContract_CustomerContractId",
                        column: x => x.CustomerContractId,
                        principalTable: "CustomerContract",
                        principalColumn: "CustomerContractId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerContractLines_SubProduct_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProduct",
                        principalColumn: "SubproductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerContractLines_ElementoConfiguracion_TipoCobroId",
                        column: x => x.TipoCobroId,
                        principalTable: "ElementoConfiguracion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContractLines_CustomerContractId",
                table: "CustomerContractLines",
                column: "CustomerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContractLines_SubProductId",
                table: "CustomerContractLines",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContractLines_TipoCobroId",
                table: "CustomerContractLines",
                column: "TipoCobroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerContractLines");
        }
    }
}
