using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class GrupoElementoConfiguracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameContract",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TypeContractId",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ElementoConfiguracion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Idconfiguracion = table.Column<long>(nullable: true),
                    Valordecimal = table.Column<double>(nullable: true),
                    Valorstring = table.Column<string>(nullable: true),
                    Valorstring2 = table.Column<string>(nullable: true),
                    Formula = table.Column<string>(nullable: true),
                    Fechacreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementoConfiguracion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrupoConfiguracion",
                columns: table => new
                {
                    IdConfiguracion = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombreconfiguracion = table.Column<string>(nullable: true),
                    Tipoconfiguracion = table.Column<string>(nullable: true),
                    IdConfiguracionorigen = table.Column<long>(nullable: true),
                    IdConfiguraciondestino = table.Column<long>(nullable: true),
                    IdZona = table.Column<long>(nullable: false),
                    Fechacreacion = table.Column<DateTime>(nullable: false),
                    Fechamodificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoConfiguracion", x => x.IdConfiguracion);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElementoConfiguracion");

            migrationBuilder.DropTable(
                name: "GrupoConfiguracion");

            migrationBuilder.DropColumn(
                name: "NameContract",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "TypeContractId",
                table: "SalesOrder");
        }
    }
}
