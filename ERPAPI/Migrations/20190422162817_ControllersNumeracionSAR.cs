using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ControllersNumeracionSAR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NumeracionSAR",
                table: "NumeracionSAR");

            migrationBuilder.AlterColumn<long>(
                name: "IdCAI",
                table: "NumeracionSAR",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<long>(
                name: "IdNumeracion",
                table: "NumeracionSAR",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumeracionSAR",
                table: "NumeracionSAR",
                column: "IdNumeracion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NumeracionSAR",
                table: "NumeracionSAR");

            migrationBuilder.DropColumn(
                name: "IdNumeracion",
                table: "NumeracionSAR");

            migrationBuilder.AlterColumn<long>(
                name: "IdCAI",
                table: "NumeracionSAR",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumeracionSAR",
                table: "NumeracionSAR",
                column: "IdCAI");
        }
    }
}
