using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedAuthSignatureGoodDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "goodsDeliveryAuthorizedSignatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GoodsDeliveryAuthorizationId = table.Column<long>(nullable: false),
                    CustomerAuthorizedSignatureId = table.Column<long>(nullable: false),
                    NombreAutorizado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goodsDeliveryAuthorizedSignatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_goodsDeliveryAuthorizedSignatures_CustomerAuthorizedSignature_CustomerAuthorizedSignatureId",
                        column: x => x.CustomerAuthorizedSignatureId,
                        principalTable: "CustomerAuthorizedSignature",
                        principalColumn: "CustomerAuthorizedSignatureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_goodsDeliveryAuthorizedSignatures_GoodsDeliveryAuthorization_GoodsDeliveryAuthorizationId",
                        column: x => x.GoodsDeliveryAuthorizationId,
                        principalTable: "GoodsDeliveryAuthorization",
                        principalColumn: "GoodsDeliveryAuthorizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_goodsDeliveryAuthorizedSignatures_CustomerAuthorizedSignatureId",
                table: "goodsDeliveryAuthorizedSignatures",
                column: "CustomerAuthorizedSignatureId");

            migrationBuilder.CreateIndex(
                name: "IX_goodsDeliveryAuthorizedSignatures_GoodsDeliveryAuthorizationId",
                table: "goodsDeliveryAuthorizedSignatures",
                column: "GoodsDeliveryAuthorizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "goodsDeliveryAuthorizedSignatures");
        }
    }
}
