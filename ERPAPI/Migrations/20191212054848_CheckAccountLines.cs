using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CheckAccountLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountManagementId",
                table: "CheckAccount",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "CheckAccountLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckAccountId = table.Column<long>(nullable: false),
                    CheckNumber = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Place = table.Column<string>(nullable: true),
                    PaytoOrderOf = table.Column<string>(nullable: true),
                    Ammount = table.Column<decimal>(nullable: false),
                    AmountWords = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckAccountLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckAccountLines_CheckAccount_CheckAccountId",
                        column: x => x.CheckAccountId,
                        principalTable: "CheckAccount",
                        principalColumn: "CheckAccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckAccount_AccountManagementId",
                table: "CheckAccount",
                column: "AccountManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckAccountLines_CheckAccountId",
                table: "CheckAccountLines",
                column: "CheckAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckAccount_AccountManagement_AccountManagementId",
                table: "CheckAccount",
                column: "AccountManagementId",
                principalTable: "AccountManagement",
                principalColumn: "AccountManagementId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckAccount_AccountManagement_AccountManagementId",
                table: "CheckAccount");

            migrationBuilder.DropTable(
                name: "CheckAccountLines");

            migrationBuilder.DropIndex(
                name: "IX_CheckAccount_AccountManagementId",
                table: "CheckAccount");            

            migrationBuilder.DropColumn(
                name: "AccountManagementId",
                table: "CheckAccount");
        }
    }
}
