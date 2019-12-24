using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Crear_Tabla_Motivos_Conciliacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MotivoConciliacion",
                columns: table => new
                                  {
                                      MotivoId = table.Column<long>(nullable: false)
                                          .Annotation("SqlServer:ValueGenerationStrategy",
                                              SqlServerValueGenerationStrategy.IdentityColumn),
                                      Nombre = table.Column<string>(nullable: false)
                                  },
                constraints: table => { table.PrimaryKey("PK_MotivoConciliacion", x => x.MotivoId); });

            migrationBuilder.DropForeignKey(
                name: "FK_ConciliacionLinea_ElementoConfiguracion_ElementoConfiguracion",
                table: "ConciliacionLinea");

            migrationBuilder.DropIndex(
                name: "IX_ConciliacionLinea_ElementoConfiguracion",
                table: "ConciliacionLinea"
            );

            migrationBuilder.DropColumn(
                name: "ElementoConfiguracion",
                table: "ConciliacionLinea");

            migrationBuilder.AddColumn<long>(
                name: "MotivoId",
                table: "ConciliacionLinea"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_ConciliacionLinea_Motivo",
                table: "ConciliacionLinea",
                column: "MotivoId",
                principalTable: "MotivoConciliacion",
                principalColumn: "MotivoId",
                onDelete: ReferentialAction.Restrict);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConciliacionLinea_Motivo",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "MotivoId",
                table: "ConciliacionLinea");

            migrationBuilder.DropTable(name: "MotivoConciliacion");

            migrationBuilder.AddColumn<long>(
                name: "ElementoConfiguracion",
                table: "ConciliacionLinea");

            migrationBuilder.CreateIndex(
                name: "IX_ConciliacionLinea_ElementoConfiguracion",
                table: "ConciliacionLinea",
                column: "ElementoConfiguracion");

            migrationBuilder.AddForeignKey(
                name: "FK_ConciliacionLinea_ElementoConfiguracion_ElementoConfiguracion",
                table: "ConciliacionLinea",
                column: "ElementoConfiguracion",
                principalTable: "ElementoConfiguracion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
