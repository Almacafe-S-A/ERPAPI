using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PeriodicidadComisionBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telefono",
                table: "Dependientes",
                newName: "TelefonoDependientes");

            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "Dependientes",
                newName: "DireccionDependientes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Dependientes",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "Edad",
                table: "Dependientes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "AplicaComision",
                table: "Departamento",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ComisionId",
                table: "Departamento",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PeridicidadId",
                table: "Departamento",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BranchPorDepartamento",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    IdDepartamento = table.Column<long>(nullable: false),
                    NombreDepartamento = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchPorDepartamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comision",
                columns: table => new
                {
                    ComisionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoComision = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comision", x => x.ComisionId);
                });

            migrationBuilder.CreateTable(
                name: "PeriodicidadPago",
                columns: table => new
                {
                    PeriodicidadId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodicidadPago", x => x.PeriodicidadId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchPorDepartamento");

            migrationBuilder.DropTable(
                name: "Comision");

            migrationBuilder.DropTable(
                name: "PeriodicidadPago");

            migrationBuilder.DropColumn(
                name: "AplicaComision",
                table: "Departamento");

            migrationBuilder.DropColumn(
                name: "ComisionId",
                table: "Departamento");

            migrationBuilder.DropColumn(
                name: "PeridicidadId",
                table: "Departamento");

            migrationBuilder.RenameColumn(
                name: "TelefonoDependientes",
                table: "Dependientes",
                newName: "Telefono");

            migrationBuilder.RenameColumn(
                name: "DireccionDependientes",
                table: "Dependientes",
                newName: "Direccion");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Dependientes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Edad",
                table: "Dependientes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
