using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class KardexTypesEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstallmentDelivery");

            migrationBuilder.DropTable(
                name: "RecipeDetail");

            migrationBuilder.DropTable(
                name: "Substratum");

            migrationBuilder.AddColumn<int>(
                name: "KardexTypeId",
                table: "Kardex",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KardexTypeId",
                table: "Kardex");

            migrationBuilder.CreateTable(
                name: "InstallmentDelivery",
                columns: table => new
                {
                    InstallmentDeliveryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentDelivery", x => x.InstallmentDeliveryId);
                });

            migrationBuilder.CreateTable(
                name: "RecipeDetail",
                columns: table => new
                {
                    IngredientCode = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Attachment = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    MaterialId = table.Column<long>(nullable: false),
                    MaterialType = table.Column<string>(nullable: true),
                    NumCara = table.Column<int>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    RecipeId = table.Column<long>(nullable: false),
                    Thickness = table.Column<double>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Width = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeDetail", x => x.IngredientCode);
                });

            migrationBuilder.CreateTable(
                name: "Substratum",
                columns: table => new
                {
                    SubstratumId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Estatus = table.Column<string>(nullable: true),
                    EstatusId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    SubstratumCode = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substratum", x => x.SubstratumId);
                });
        }
    }
}
