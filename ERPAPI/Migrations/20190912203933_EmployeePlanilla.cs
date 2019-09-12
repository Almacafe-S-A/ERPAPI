using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class EmployeePlanilla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_ApplicationUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Escala_IdEscala",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Planilla");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdEscala",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdEscala",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdPlanilla",
                table: "Employees");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateTable(
                name: "TipoPlanillas",
                columns: table => new
                {
                    IdTipoPlanilla = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoPlanilla = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPlanillas", x => x.IdTipoPlanilla);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_ApplicationUserId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "TipoPlanillas");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEscala",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "IdPlanilla",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Planilla",
                columns: table => new
                {
                    IdPlanilla = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    TipoPlanilla = table.Column<string>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planilla", x => x.IdPlanilla);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdEscala",
                table: "Employees",
                column: "IdEscala");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Escala_IdEscala",
                table: "Employees",
                column: "IdEscala",
                principalTable: "Escala",
                principalColumn: "IdEscala",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
