using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ERPAPI.Migrations
{
    public partial class QuotationKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "QuotationDetail");

            migrationBuilder.DropTable(
               name: "Quotation");

            migrationBuilder.CreateTable(
                name: "Quotation",
                columns: table => new
                {
                    QuotationCode = table.Column<long>(nullable: false),
                    QuotationVersion = table.Column<long>(nullable: false),
                    TipoId = table.Column<long>(nullable: true),
                    QuotationDate = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Representative = table.Column<string>(nullable: true),
                    BranchCode = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotation", x => new { x.QuotationCode, x.QuotationVersion });
                    table.UniqueConstraint("AK_Quotation_QuotationCode", x => x.QuotationCode);
                    table.ForeignKey(
                        name: "FK_Quotation_ElementoConfiguracion_TipoId",
                        column: x => x.TipoId,
                        principalTable: "ElementoConfiguracion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuotationDetail",
                columns: table => new
                {
                    QuotationDetailId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuotationCode = table.Column<long>(nullable: false),
                    QuotationVersion = table.Column<long>(nullable: false),
                    RecipeId = table.Column<long>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Width = table.Column<double>(nullable: false),
                    Thickness = table.Column<double>(nullable: false),
                    NumCara = table.Column<double>(nullable: false),
                    Attachment = table.Column<string>(nullable: true),
                    MaterialId = table.Column<long>(nullable: false),
                    MaterialType = table.Column<string>(nullable: true),
                    PaymentTypesId = table.Column<long>(nullable: false),
                    ApplyTax = table.Column<bool>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false),
                    AdvancePaymentPercent = table.Column<double>(nullable: false),
                    QuotationDueDate = table.Column<DateTime>(nullable: false),
                    Observations = table.Column<string>(nullable: true),
                    InstalationAddress = table.Column<string>(nullable: true),
                    DeliveryTerm = table.Column<string>(nullable: true),
                    QuantityInLetters = table.Column<string>(nullable: true),
                    Design = table.Column<string>(nullable: true),
                    ColorArrangement = table.Column<string>(nullable: true),
                    Publicity = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    StructuralMeasurement = table.Column<double>(nullable: false),
                    IntallationForm = table.Column<string>(nullable: true),
                    HorizontalTube = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    BoxMaterial = table.Column<string>(nullable: true),
                    InstallationObservation = table.Column<string>(nullable: true),
                    LetterSize = table.Column<double>(nullable: false),
                    LetterRief = table.Column<string>(nullable: true),
                    LetterEmbossed = table.Column<string>(nullable: true),
                    LetterFlat = table.Column<string>(nullable: true),
                    ConcreteBase = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationDetail", x => new { x.QuotationDetailId, x.QuotationCode });
                    table.UniqueConstraint("AK_QuotationDetail_QuotationDetailId", x => x.QuotationDetailId);
                    table.ForeignKey(
                        name: "FK_QuotationDetail_Quotation_QuotationCode_QuotationVersion",
                        columns: x => new { x.QuotationCode, x.QuotationVersion },
                        principalTable: "Quotation",
                        principalColumns: new[] { "QuotationCode", "QuotationVersion" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_TipoId",
                table: "Quotation",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationDetail_QuotationCode_QuotationVersion",
                table: "QuotationDetail",
                columns: new[] { "QuotationCode", "QuotationVersion" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuotationDetail");

            migrationBuilder.DropTable(
                name: "Quotation");
        }
    }
}
