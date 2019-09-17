using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AceptarNulos_LlavesForaneas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_City_CityId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Country_CountryId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CustomerType_CustomerTypeId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Estados_IdEstado",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_StateId",
                table: "Customer");

            migrationBuilder.AlterColumn<long>(
                name: "StateId",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "IdEstado",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "CustomerTypeId",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "CountryId",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "CityId",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Customer_StateId",
                table: "Customer",
                column: "StateId",
                unique: true,
                filter: "[StateId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_City_CityId",
                table: "Customer",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Country_CountryId",
                table: "Customer",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_CustomerType_CustomerTypeId",
                table: "Customer",
                column: "CustomerTypeId",
                principalTable: "CustomerType",
                principalColumn: "CustomerTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Estados_IdEstado",
                table: "Customer",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_City_CityId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Country_CountryId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CustomerType_CustomerTypeId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Estados_IdEstado",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_StateId",
                table: "Customer");

            migrationBuilder.AlterColumn<long>(
                name: "StateId",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "IdEstado",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CustomerTypeId",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CountryId",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CityId",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_StateId",
                table: "Customer",
                column: "StateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_City_CityId",
                table: "Customer",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Country_CountryId",
                table: "Customer",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_CustomerType_CustomerTypeId",
                table: "Customer",
                column: "CustomerTypeId",
                principalTable: "CustomerType",
                principalColumn: "CustomerTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Estados_IdEstado",
                table: "Customer",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
