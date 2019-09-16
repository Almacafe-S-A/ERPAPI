using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Vendor_FKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Vendor");

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "Vendor",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "Vendor",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "StateId",
                table: "Vendor",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CityId",
                table: "Vendor",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CountryId",
                table: "Vendor",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CurrencyId",
                table: "Vendor",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_IdEstado",
                table: "Vendor",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_StateId",
                table: "Vendor",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_City_CityId",
                table: "Vendor",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Country_CountryId",
                table: "Vendor",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Currency_CurrencyId",
                table: "Vendor",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Estados_IdEstado",
                table: "Vendor",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_State_StateId",
                table: "Vendor",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_City_CityId",
                table: "Vendor");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Country_CountryId",
                table: "Vendor");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Currency_CurrencyId",
                table: "Vendor");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Estados_IdEstado",
                table: "Vendor");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_State_StateId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_CityId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_CountryId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_CurrencyId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_IdEstado",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_StateId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Vendor");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Vendor",
                nullable: true);
        }
    }
}
