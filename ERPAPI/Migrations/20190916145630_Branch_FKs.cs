using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Branch_FKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "StateId",
                table: "Branch",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "CountryId",
                table: "Branch",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "CityId",
                table: "Branch",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CityId",
                table: "Branch",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CountryId",
                table: "Branch",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CurrencyId",
                table: "Branch",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_IdEstado",
                table: "Branch",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_StateId",
                table: "Branch",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_City_CityId",
                table: "Branch",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Country_CountryId",
                table: "Branch",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Currency_CurrencyId",
                table: "Branch",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Estados_IdEstado",
                table: "Branch",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_State_StateId",
                table: "Branch",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_City_CityId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Country_CountryId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Currency_CurrencyId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Estados_IdEstado",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_State_StateId",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_CityId",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_CountryId",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_CurrencyId",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_IdEstado",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_StateId",
                table: "Branch");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Branch",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Branch",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Branch",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
