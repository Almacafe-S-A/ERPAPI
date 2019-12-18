using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class QuotationDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuotationDetail");
        }
    }
}
