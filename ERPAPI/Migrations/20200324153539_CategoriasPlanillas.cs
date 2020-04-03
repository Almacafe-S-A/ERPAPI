using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CategoriasPlanillas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriasPlanillas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasPlanillas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CategoriasPlanillas",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1L, "NOMINA" },
                    { 2L, "NOMINA CONFIDENCIAL" },
                    { 3L, "NOMINA EXTRAORDINARIA" },
                    { 4L, "OTRO" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriasPlanillas");
        }
    }
}
