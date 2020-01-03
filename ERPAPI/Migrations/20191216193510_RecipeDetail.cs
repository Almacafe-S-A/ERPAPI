using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RecipeDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "RecipeDetail",
                columns: table => new
                {
                    IngredientCode = table.Column<long>(nullable: false),
                    RecipeId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Width = table.Column<double>(nullable: false),
                    Thickness = table.Column<double>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    NumCara = table.Column<int>(nullable: false),
                    Attachment = table.Column<string>(nullable: true),
                    MaterialId = table.Column<long>(nullable: false),
                    MaterialType = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeDetail", x => new { x.IngredientCode, x.RecipeId });
                    table.UniqueConstraint("AK_RecipeDetail_IngredientCode", x => x.IngredientCode);
                    table.ForeignKey(
                        name: "FK_RecipeDetail_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetail_RecipeId",
                table: "RecipeDetail",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.DropTable(
                name: "RecipeDetail");

            }
    }
}
