using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Nuevoscambios09082019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dimensions_Num_DimCode",
                table: "Dimensions");

            migrationBuilder.CreateTable(
                name: "CenterCoste",
                columns: table => new
                {
                    NumId = table.Column<int>(maxLength: 30, nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 60, nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterCoste", x => x.NumId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CenterCoste");

            migrationBuilder.CreateIndex(
                name: "IX_Dimensions_Num_DimCode",
                table: "Dimensions",
                columns: new[] { "Num", "DimCode" },
                unique: true);
        }
    }
}
