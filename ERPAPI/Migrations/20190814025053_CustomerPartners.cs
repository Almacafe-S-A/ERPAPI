using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerPartners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastPasswordChangedDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CustomerPartners",
                columns: table => new
                {
                    PartnerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PartnerName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    Identidad = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPartners", x => x.PartnerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerPartners");

            migrationBuilder.DropColumn(
                name: "LastPasswordChangedDate",
                table: "AspNetUsers");
        }
    }
}
